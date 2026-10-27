using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GrafosApp
{
    public class Arista
    {
        public string De { get; private set; }
        public string A { get; private set; }
        public double Peso { get; private set; }

        public Arista(string de, string a, double peso)
        {
            De = de; A = a; Peso = peso;
        }
    }

    public class Grafo
    {
        public bool Dirigido { get; private set; }
        public List<string> Vertices { get; private set; }
        public List<Arista> Aristas { get; private set; }

        public Grafo(bool dirigido)
        {
            Dirigido = dirigido;
            Vertices = new List<string>();
            Aristas = new List<Arista>();
        }

        static readonly char[] Separadores = { ' ', ',', '\t', '-', '>', ';', '\r' };

        public static Grafo Parsear(string texto, bool dirigido)
        {
            var g = new Grafo(dirigido);
            foreach (var linea in texto.Split('\n'))
            {
                var p = linea.Trim().Split(Separadores, StringSplitOptions.RemoveEmptyEntries);
                if (p.Length == 0) continue;
                if (p.Length == 1) { g.AgregarVertice(p[0]); continue; }
                double peso = 1;
                if (p.Length >= 3 && !double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out peso))
                    throw new FormatException("Peso inválido en la línea: " + linea.Trim());
                g.AgregarArista(p[0], p[1], peso);
            }
            return g;
        }

        public void AgregarVertice(string v) { if (!Vertices.Contains(v)) Vertices.Add(v); }

        public void AgregarArista(string a, string b, double peso = 1)
        {
            AgregarVertice(a); AgregarVertice(b);
            Aristas.Add(new Arista(a, b, peso));
        }

        public IEnumerable<Tuple<string, double>> Vecinos(string v)
        {
            foreach (var e in Aristas)
            {
                if (e.De == v) yield return Tuple.Create(e.A, e.Peso);
                else if (!Dirigido && e.A == v) yield return Tuple.Create(e.De, e.Peso);
            }
        }

        public int[,] Matriz()
        {
            int n = Vertices.Count;
            var m = new int[n, n];
            foreach (var e in Aristas)
            {
                int i = Vertices.IndexOf(e.De), j = Vertices.IndexOf(e.A);
                m[i, j]++;
                if (!Dirigido && i != j) m[j, i]++;
            }
            return m;
        }

        public string MatrizTexto()
        {
            var m = Matriz();
            int n = Vertices.Count;
            var sb = new StringBuilder();
            sb.Append("    ");
            foreach (var v in Vertices) sb.Append(v.PadLeft(3));
            sb.AppendLine();
            for (int i = 0; i < n; i++)
            {
                sb.Append(Vertices[i].PadLeft(3)).Append(' ');
                for (int j = 0; j < n; j++) sb.Append(m[i, j].ToString().PadLeft(3));
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public int Grado(string v)
        {
            int d = 0;
            foreach (var e in Aristas) { if (e.De == v) d++; if (e.A == v) d++; }
            return d;
        }
        public int GradoSalida(string v) { return Aristas.Count(e => e.De == v); }
        public int GradoEntrada(string v) { return Aristas.Count(e => e.A == v); }

        public string GradosTexto()
        {
            var sb = new StringBuilder();
            if (!Dirigido)
            {
                foreach (var v in Vertices) sb.AppendLine("  deg(" + v + ") = " + Grado(v));
                int suma = Vertices.Sum(v => Grado(v));
                sb.AppendLine("  Σ deg = " + suma + "  |  2·|E| = " + (2 * Aristas.Count) + "  →  " + (suma == 2 * Aristas.Count ? "se cumple" : "NO se cumple"));
            }
            else
            {
                sb.AppendLine("  Vértice  d-(entrada)  d+(salida)");
                foreach (var v in Vertices)
                    sb.AppendLine("  " + v.PadRight(8) + " " + GradoEntrada(v).ToString().PadRight(12) + " " + GradoSalida(v));
                sb.AppendLine("  Totales: entrada = " + Aristas.Count + ", salida = " + Aristas.Count);
            }
            return sb.ToString();
        }

        public string SecuenciaGrados()
        {
            IEnumerable<string> seq;
            if (Dirigido)
                seq = Vertices.Select(v => "(" + GradoEntrada(v) + "," + GradoSalida(v) + ")").OrderBy(x => x);
            else
                seq = Vertices.Select(v => Grado(v).ToString()).OrderByDescending(x => x);
            return "(" + string.Join(", ", seq) + ")";
        }

        IEnumerable<string> Adj(string v, bool inverso, bool ignorarDireccion)
        {
            foreach (var e in Aristas)
            {
                if (ignorarDireccion)
                {
                    if (e.De == v) yield return e.A;
                    if (e.A == v) yield return e.De;
                }
                else if (!inverso) { if (e.De == v) yield return e.A; }
                else { if (e.A == v) yield return e.De; }
            }
        }

        HashSet<string> Alcanzables(string inicio, bool inverso, bool ignorarDireccion)
        {
            var vis = new HashSet<string> { inicio };
            var cola = new Queue<string>();
            cola.Enqueue(inicio);
            while (cola.Count > 0)
            {
                var v = cola.Dequeue();
                foreach (var w in Adj(v, inverso, ignorarDireccion))
                    if (vis.Add(w)) cola.Enqueue(w);
            }
            return vis;
        }

        public bool EsConexo()
        {
            return Vertices.Count == 0 || Alcanzables(Vertices[0], false, true).Count == Vertices.Count;
        }

        public bool EsFuertementeConexo()
        {
            return Vertices.Count == 0 ||
                (Alcanzables(Vertices[0], false, false).Count == Vertices.Count &&
                 Alcanzables(Vertices[0], true, false).Count == Vertices.Count);
        }

        public string AnalisisEuler()
        {
            var activos = Vertices.Where(v => Aristas.Any(e => e.De == v || e.A == v)).ToList();
            if (activos.Count == 0) return "Sin aristas.";
            var alc = Alcanzables(activos[0], false, true);
            if (!activos.All(x => alc.Contains(x)))
                return "No es euleriano: las aristas no están en una sola componente conexa.";

            if (!Dirigido)
            {
                var impares = Vertices.Where(v => Grado(v) % 2 == 1).ToList();
                if (impares.Count == 0)
                    return "SÍ tiene circuito euleriano (todos los grados son pares).\r\nCircuito: " + Recorrido(activos[0]);
                if (impares.Count == 2)
                    return "Tiene camino euleriano (no circuito): de " + impares[0] + " a " + impares[1] + ".\r\nCamino: " + Recorrido(impares[0]);
                return "NO tiene recorrido euleriano: " + impares.Count + " vértices de grado impar (" + string.Join(", ", impares) + ").";
            }

            var dif = Vertices.ToDictionary(v => v, v => GradoSalida(v) - GradoEntrada(v));
            if (dif.Values.All(d => d == 0))
                return "SÍ tiene circuito euleriano dirigido (entrada = salida en todos).\r\nCircuito: " + Recorrido(activos[0]);
            if (dif.Values.Count(d => d == 1) == 1 && dif.Values.Count(d => d == -1) == 1 &&
                dif.Values.Count(d => d == 0) == Vertices.Count - 2)
            {
                var ini = dif.First(kv => kv.Value == 1).Key;
                var fin = dif.First(kv => kv.Value == -1).Key;
                return "Tiene camino euleriano dirigido de " + ini + " a " + fin + ".\r\nCamino: " + Recorrido(ini);
            }
            return "NO tiene recorrido euleriano dirigido.";
        }

        string Recorrido(string inicio)
        {
            var usada = new bool[Aristas.Count];
            var pila = new Stack<string>();
            var res = new List<string>();
            pila.Push(inicio);
            while (pila.Count > 0)
            {
                var v = pila.Peek();
                int idx = -1;
                for (int i = 0; i < Aristas.Count; i++)
                {
                    if (usada[i]) continue;
                    var e = Aristas[i];
                    if (e.De == v || (!Dirigido && e.A == v)) { idx = i; break; }
                }
                if (idx < 0) res.Add(pila.Pop());
                else
                {
                    usada[idx] = true;
                    var e = Aristas[idx];
                    pila.Push(e.De == v ? e.A : e.De);
                }
            }
            res.Reverse();
            return string.Join(" → ", res);
        }

        public List<string> Hamilton(bool ciclo)
        {
            if (Vertices.Count == 0) return null;
            var inicios = ciclo ? new List<string> { Vertices[0] } : Vertices.ToList();
            foreach (var s in inicios)
            {
                var camino = new List<string> { s };
                var vis = new HashSet<string> { s };
                if (BuscarHamilton(camino, vis, ciclo)) return camino;
            }
            return null;
        }

        bool BuscarHamilton(List<string> c, HashSet<string> vis, bool ciclo)
        {
            string ultimo = c[c.Count - 1];
            if (c.Count == Vertices.Count)
            {
                if (!ciclo) return true;
                return Vertices.Count >= (Dirigido ? 2 : 3) && Vecinos(ultimo).Any(x => x.Item1 == c[0]);
            }
            foreach (var t in Vecinos(ultimo).ToList())
            {
                string v = t.Item1;
                if (vis.Contains(v)) continue;
                c.Add(v); vis.Add(v);
                if (BuscarHamilton(c, vis, ciclo)) return true;
                c.RemoveAt(c.Count - 1); vis.Remove(v);
            }
            return false;
        }

        public static Dictionary<string, string> Isomorfismo(Grafo a, Grafo b)
        {
            if (a.Dirigido != b.Dirigido || a.Vertices.Count != b.Vertices.Count ||
                a.Aristas.Count != b.Aristas.Count) return null;
            if (a.SecuenciaGrados() != b.SecuenciaGrados()) return null;

            var ma = a.Matriz(); var mb = b.Matriz();
            int n = a.Vertices.Count;
            var perm = new int[n];
            var usado = new bool[n];

            bool Rec(int k)
            {
                if (k == n) return true;
                for (int c = 0; c < n; c++)
                {
                    if (usado[c]) continue;
                    bool ok = true;
                    for (int j = 0; j <= k && ok; j++)
                    {
                        int pj = j == k ? c : perm[j];
                        if (ma[k, j] != mb[c, pj] || ma[j, k] != mb[pj, c]) ok = false;
                    }
                    if (!ok) continue;
                    usado[c] = true; perm[k] = c;
                    if (Rec(k + 1)) return true;
                    usado[c] = false;
                }
                return false;
            }

            if (!Rec(0)) return null;
            var f = new Dictionary<string, string>();
            for (int i = 0; i < n; i++) f[a.Vertices[i]] = b.Vertices[perm[i]];
            return f;
        }

        public bool EsArbol()
        {
            return Vertices.Count > 0 && EsConexo() && Aristas.Count == Vertices.Count - 1;
        }

        List<string> Hijos(string v, string padre)
        {
            return Vecinos(v).Select(x => x.Item1).Where(x => x != padre).Distinct().ToList();
        }

        public List<string> Hojas(string raiz)
        {
            var res = new List<string>();
            void Rec(string v, string p)
            {
                var h = Hijos(v, p);
                if (h.Count == 0) res.Add(v);
                foreach (var x in h) Rec(x, v);
            }
            Rec(raiz, null);
            return res;
        }

        public int Altura(string raiz)
        {
            int Rec(string v, string p)
            {
                var h = Hijos(v, p);
                return h.Count == 0 ? 0 : 1 + h.Max(x => Rec(x, v));
            }
            return Rec(raiz, null);
        }

        public List<string> Preorden(string raiz)
        {
            var res = new List<string>();
            void Rec(string v, string p)
            {
                res.Add(v);
                foreach (var h in Hijos(v, p)) Rec(h, v);
            }
            Rec(raiz, null);
            return res;
        }

        public List<string> Inorden(string raiz)
        {
            var res = new List<string>();
            void Rec(string v, string p)
            {
                var h = Hijos(v, p);
                if (h.Count > 0) Rec(h[0], v);
                res.Add(v);
                for (int i = 1; i < h.Count; i++) Rec(h[i], v);
            }
            Rec(raiz, null);
            return res;
        }

        public List<string> Postorden(string raiz)
        {
            var res = new List<string>();
            void Rec(string v, string p)
            {
                foreach (var h in Hijos(v, p)) Rec(h, v);
                res.Add(v);
            }
            Rec(raiz, null);
            return res;
        }

        public Tuple<List<Arista>, double> Kruskal()
        {
            var padre = Vertices.ToDictionary(v => v, v => v);
            string Find(string x)
            {
                while (padre[x] != x) { padre[x] = padre[padre[x]]; x = padre[x]; }
                return x;
            }
            var arbol = new List<Arista>();
            foreach (var e in Aristas.OrderBy(a => a.Peso))
            {
                var ra = Find(e.De); var rb = Find(e.A);
                if (ra == rb) continue;
                padre[ra] = rb;
                arbol.Add(e);
            }
            return Tuple.Create(arbol, arbol.Sum(a => a.Peso));
        }

        public void Dijkstra(string origen, out Dictionary<string, double> dist, out Dictionary<string, string> prev)
        {
            dist = Vertices.ToDictionary(v => v, v => double.PositiveInfinity);
            prev = Vertices.ToDictionary(v => v, v => (string)null);
            var visto = new HashSet<string>();
            dist[origen] = 0;
            while (true)
            {
                string u = null;
                foreach (var v in Vertices)
                    if (!visto.Contains(v) && !double.IsPositiveInfinity(dist[v]) && (u == null || dist[v] < dist[u]))
                        u = v;
                if (u == null) break;
                visto.Add(u);
                foreach (var t in Vecinos(u))
                {
                    string v = t.Item1; double w = t.Item2;
                    if (dist[u] + w < dist[v]) { dist[v] = dist[u] + w; prev[v] = u; }
                }
            }
        }

        public static List<string> Camino(Dictionary<string, string> prev, string destino)
        {
            var c = new List<string>();
            for (string v = destino; v != null; v = prev[v]) c.Insert(0, v);
            return c;
        }
    }

    public class CaminoRed
    {
        public List<string> Ruta { get; private set; }
        public int Duracion { get; private set; }
        public CaminoRed(List<string> ruta, int duracion) { Ruta = ruta; Duracion = duracion; }
    }

    public class Proyecto
    {
        public List<string> Orden { get; private set; }
        public Dictionary<string, int> Duracion { get; private set; }
        public Dictionary<string, List<string>> Pred { get; private set; }

        public Proyecto()
        {
            Orden = new List<string>();
            Duracion = new Dictionary<string, int>();
            Pred = new Dictionary<string, List<string>>();
        }

        public static Proyecto Parsear(string texto)
        {
            var p = new Proyecto();
            foreach (var linea in texto.Split('\n'))
            {
                var t = linea.Trim().Split(new[] { ' ', ',', '\t', ';', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (t.Length == 0) continue;
                int dur;
                if (t.Length < 2 || !int.TryParse(t[1], out dur))
                    throw new FormatException("Línea inválida: " + linea.Trim());
                p.Orden.Add(t[0]);
                p.Duracion[t[0]] = dur;
                p.Pred[t[0]] = t.Skip(2)
                    .Where(x => x != "-" && x != "—"
                             && !x.Equals("ninguna", StringComparison.OrdinalIgnoreCase)
                             && !x.Equals("ninguno", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            foreach (var kv in p.Pred)
                foreach (var x in kv.Value)
                    if (!p.Duracion.ContainsKey(x))
                        throw new FormatException("La predecesora '" + x + "' de " + kv.Key + " no está definida.");
            return p;
        }

        public Dictionary<string, List<string>> Sucesoras()
        {
            return Orden.ToDictionary(a => a, a => Orden.Where(b => Pred[b].Contains(a)).ToList());
        }

        public Dictionary<string, int> Niveles()
        {
            var memo = new Dictionary<string, int>();
            int Nivel(string a, HashSet<string> enCurso)
            {
                int n;
                if (memo.TryGetValue(a, out n)) return n;
                if (!enCurso.Add(a)) throw new InvalidOperationException("La red tiene un ciclo.");
                int r = Pred[a].Count == 0 ? 0 : 1 + Pred[a].Max(x => Nivel(x, enCurso));
                enCurso.Remove(a);
                memo[a] = r;
                return r;
            }
            foreach (var a in Orden) Nivel(a, new HashSet<string>());
            return memo;
        }

        public List<CaminoRed> Caminos()
        {
            Niveles();
            var suc = Sucesoras();
            var res = new List<CaminoRed>();
            void Rec(List<string> cam)
            {
                var ult = cam[cam.Count - 1];
                if (suc[ult].Count == 0)
                {
                    res.Add(new CaminoRed(new List<string>(cam), cam.Sum(a => Duracion[a])));
                    return;
                }
                foreach (var s in suc[ult]) { cam.Add(s); Rec(cam); cam.RemoveAt(cam.Count - 1); }
            }
            foreach (var a in Orden.Where(x => Pred[x].Count == 0)) Rec(new List<string> { a });
            return res.OrderByDescending(r => r.Duracion).ToList();
        }
    }
}