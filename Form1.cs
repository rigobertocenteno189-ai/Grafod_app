using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrafosApp;   // namespace de Grafo.cs

namespace Grafos_app
{
    public partial class Form1 : Form
    {
        Grafo grafo;
        readonly HashSet<Tuple<string, string>> resaltadas = new HashSet<Tuple<string, string>>();
        bool flechasEnCamino;
        Proyecto proyecto;
        HashSet<Tuple<string, string>> criticas = new HashSet<Tuple<string, string>>();
        const float R = 20;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Leer();
        }

        // ---------------- Leer el grafo ----------------
        Grafo Leer()
        {
            try
            {
                grafo = Grafo.Parsear(txtAristas.Text, chkDirigido.Checked);
                resaltadas.Clear();
                flechasEnCamino = false;
                picGrafo.Invalidate();
                if (grafo.Vertices.Count == 0) { txtRes.Text = "Escribe al menos una arista."; return null; }
                return grafo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en los datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // ---------------- Botones ----------------
        private void btnMatriz_Click(object sender, EventArgs e)
        {
            var g = Leer(); if (g == null) return;
            var sb = new StringBuilder();
            sb.AppendLine(g.Dirigido ? "DÍGRAFO" : "GRAFO NO DIRIGIDO");
            sb.AppendLine("|V| = " + g.Vertices.Count + "   |E| = " + g.Aristas.Count + "\r\n");
            sb.AppendLine("Matriz de adyacencia:");
            sb.AppendLine(g.MatrizTexto());
            sb.AppendLine(g.Dirigido ? "Grados de entrada y salida:" : "Grados:");
            sb.AppendLine(g.GradosTexto());
            sb.AppendLine("Secuencia de grados: " + g.SecuenciaGrados());
            if (g.Dirigido)
            {
                sb.AppendLine("Débilmente conexo: " + (g.EsConexo() ? "sí" : "no"));
                sb.AppendLine("Fuertemente conexo: " + (g.EsFuertementeConexo() ? "sí" : "no"));
            }
            else
            {
                sb.AppendLine("Conexo: " + (g.EsConexo() ? "sí" : "no"));
            }
            txtRes.Text = sb.ToString();
        }

        private void btnEuler_Click(object sender, EventArgs e)
        {
            var g = Leer(); if (g == null) return;
            var sb = new StringBuilder();
            sb.AppendLine("== EULER (recorre todas las aristas) ==");
            sb.AppendLine(g.AnalisisEuler());
            sb.AppendLine();
            sb.AppendLine("== HAMILTON (visita todos los vértices) ==");
            var cam = g.Hamilton(false);
            sb.AppendLine(cam == null ? "Camino hamiltoniano: NO existe." : "Camino hamiltoniano: " + string.Join(" → ", cam));
            var cic = g.Hamilton(true);
            sb.AppendLine(cic == null ? "Ciclo hamiltoniano: NO existe." : "Ciclo hamiltoniano: " + string.Join(" → ", cic) + " → " + cic[0]);
            txtRes.Text = sb.ToString();
        }

        private void btnArbol_Click(object sender, EventArgs e)
        {
            var g = Leer(); if (g == null) return;
            var sb = new StringBuilder();
            string raiz = string.IsNullOrWhiteSpace(txtRaiz.Text) ? g.Vertices[0] : txtRaiz.Text.Trim();
            if (!g.Vertices.Contains(raiz)) { txtRes.Text = "La raíz '" + raiz + "' no existe."; return; }
            if (!g.EsArbol())
            {
                sb.AppendLine("NO es un árbol.");
                sb.AppendLine("Un árbol es conexo y cumple |E| = |V| − 1.");
                sb.AppendLine("Aquí: conexo = " + (g.EsConexo() ? "sí" : "no") + ",  |E| = " + g.Aristas.Count + ",  |V| − 1 = " + (g.Vertices.Count - 1));
                txtRes.Text = sb.ToString();
                return;
            }
            var hojas = g.Hojas(raiz);
            sb.AppendLine("Es un ÁRBOL (raíz = " + raiz + ").");
            sb.AppendLine("Número de aristas: " + g.Aristas.Count + " (= |V| − 1)");
            sb.AppendLine("Hojas (" + hojas.Count + "): " + string.Join(", ", hojas));
            sb.AppendLine("Altura: " + g.Altura(raiz));
            sb.AppendLine("Preorden:  " + string.Join(", ", g.Preorden(raiz)));
            sb.AppendLine("Inorden:   " + string.Join(", ", g.Inorden(raiz)));
            sb.AppendLine("Postorden: " + string.Join(", ", g.Postorden(raiz)));
            sb.AppendLine();
            sb.AppendLine("Nota: el inorden toma el primer hijo como izquierdo; el orden de hijos es el orden en que escribiste las aristas.");
            txtRes.Text = sb.ToString();
        }

        private void btnKruskal_Click(object sender, EventArgs e)
        {
            var g = Leer(); if (g == null) return;
            if (!g.EsConexo()) { txtRes.Text = "El grafo no es conexo: Kruskal daría un bosque, no un árbol."; return; }
            var r = g.Kruskal();
            var arbol = r.Item1;
            double peso = r.Item2;
            var sb = new StringBuilder("== KRUSKAL ==\r\nAristas ordenadas por peso:\r\n");
            foreach (var ar in g.Aristas.OrderBy(a => a.Peso))
                sb.AppendLine("  " + ar.De + ar.A + " (" + ar.Peso + ")");
            sb.AppendLine("\r\nÁrbol generador mínimo:");
            foreach (var ar in arbol)
            {
                sb.AppendLine("  " + ar.De + ar.A + " (" + ar.Peso + ")");
                resaltadas.Add(Tuple.Create(ar.De, ar.A));
            }
            sb.AppendLine("\r\nPeso total = " + string.Join(" + ", arbol.Select(a => a.Peso.ToString())) + " = " + peso);
            txtRes.Text = sb.ToString();
            picGrafo.Invalidate();
        }

        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            var g = Leer(); if (g == null) return;
            string o = txtOrigen.Text.Trim(), d = txtDestino.Text.Trim();
            if (!g.Vertices.Contains(o) || !g.Vertices.Contains(d)) { txtRes.Text = "Origen o destino no existen."; return; }
            if (g.Aristas.Any(a => a.Peso < 0)) { txtRes.Text = "Dijkstra no admite pesos negativos."; return; }
            Dictionary<string, double> dist;
            Dictionary<string, string> prev;
            g.Dijkstra(o, out dist, out prev);
            var sb = new StringBuilder("== DIJKSTRA desde " + o + " ==\r\n");
            foreach (var v in g.Vertices)
                sb.AppendLine("  d(" + v + ") = " + (double.IsPositiveInfinity(dist[v]) ? "∞" : dist[v].ToString()));
            if (double.IsPositiveInfinity(dist[d])) sb.AppendLine("\r\nNo hay camino de " + o + " a " + d + ".");
            else
            {
                var cam = Grafo.Camino(prev, d);
                sb.AppendLine("\r\nCamino más corto: " + string.Join(" → ", cam));
                sb.AppendLine("Distancia total: " + dist[d]);
                for (int i = 0; i + 1 < cam.Count; i++) resaltadas.Add(Tuple.Create(cam[i], cam[i + 1]));
                flechasEnCamino = true;
            }
            txtRes.Text = sb.ToString();
            picGrafo.Invalidate();
        }

        private void btnIso_Click(object sender, EventArgs e)
        {
            var ga = Leer(); if (ga == null) return;
            Grafo gb;
            try { gb = Grafo.Parsear(txtGrafo2.Text, chkDirigido.Checked); }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }

            var sb = new StringBuilder("== ISOMORFISMO ==\r\nInvariantes:\r\n");
            sb.AppendLine("  |V|: " + ga.Vertices.Count + " vs " + gb.Vertices.Count);
            sb.AppendLine("  |E|: " + ga.Aristas.Count + " vs " + gb.Aristas.Count);
            sb.AppendLine("  Secuencia de grados: " + ga.SecuenciaGrados() + " vs " + gb.SecuenciaGrados());
            var f = Grafo.Isomorfismo(ga, gb);
            if (f == null) sb.AppendLine("\r\nResultado: NO son isomorfos.");
            else
            {
                sb.AppendLine("\r\nResultado: SÍ son isomorfos. Biyección f:");
                foreach (var kv in f) sb.AppendLine("  f(" + kv.Key + ") = " + kv.Value);
            }
            txtRes.Text = sb.ToString();
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            try
            {
                proyecto = Proyecto.Parsear(txtActividades.Text);
                var caminos = proyecto.Caminos();
                if (caminos.Count == 0) { txtResRed.Text = "Sin actividades."; return; }
                int max = caminos.Max(c => c.Duracion);
                var sb = new StringBuilder("Caminos de la red:\r\n");
                foreach (var c in caminos)
                {
                    sb.AppendLine("  " + string.Join(" → ", c.Ruta) + "  =  "
                        + string.Join("+", c.Ruta.Select(x => proyecto.Duracion[x].ToString()))
                        + " = " + c.Duracion + " días" + (c.Duracion == max ? "   ← CRÍTICO" : ""));
                }
                var crit = caminos.First(c => c.Duracion == max).Ruta;
                sb.AppendLine("\r\nCamino crítico: " + string.Join(" → ", crit));
                sb.AppendLine("Duración mínima del proyecto: " + max + " días");
                txtResRed.Text = sb.ToString();

                criticas = new HashSet<Tuple<string, string>>();
                foreach (var c in caminos.Where(x => x.Duracion == max))
                    for (int i = 0; i + 1 < c.Ruta.Count; i++)
                        criticas.Add(Tuple.Create(c.Ruta[i], c.Ruta[i + 1]));
                picRed.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en la red", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ---------------- Dibujo del grafo ----------------
        private void picGrafo_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            if (grafo == null || grafo.Vertices.Count == 0)
            {
                g.DrawString("Escribe las aristas y pulsa un botón", Font, Brushes.Gray, 20, 20);
                return;
            }

            float cx = picGrafo.ClientSize.Width / 2f, cy = picGrafo.ClientSize.Height / 2f;
            float radio = Math.Max(30, Math.Min(cx, cy) - 60);
            var pos = new Dictionary<string, PointF>();
            int n = grafo.Vertices.Count;
            for (int i = 0; i < n; i++)
            {
                double ang = 2 * Math.PI * i / n - Math.PI / 2;
                pos[grafo.Vertices[i]] = n == 1
                    ? new PointF(cx, cy)
                    : new PointF(cx + radio * (float)Math.Cos(ang), cy + radio * (float)Math.Sin(ang));
            }

            bool hayPesos = grafo.Aristas.Any(x => x.Peso != 1);
            foreach (var ar in grafo.Aristas)
            {
                var p1 = pos[ar.De]; var p2 = pos[ar.A];
                bool directo = resaltadas.Contains(Tuple.Create(ar.De, ar.A));
                bool inverso = !grafo.Dirigido && resaltadas.Contains(Tuple.Create(ar.A, ar.De));
                bool res = directo || inverso;
                if (inverso && !directo) { var tmp = p1; p1 = p2; p2 = tmp; }

                using (var pen = new Pen(res ? Color.Crimson : Color.DimGray, res ? 3.5f : 1.8f))
                {
                    if (ar.De == ar.A)
                    {
                        g.DrawEllipse(pen, p1.X - 12, p1.Y - R - 26, 24, 30);
                        continue;
                    }
                    float dx = p2.X - p1.X, dy = p2.Y - p1.Y;
                    float len = (float)Math.Sqrt(dx * dx + dy * dy);
                    var pa = new PointF(p1.X + dx / len * R, p1.Y + dy / len * R);
                    var pb = new PointF(p2.X - dx / len * R, p2.Y - dy / len * R);
                    if (res && flechasEnCamino) pen.CustomEndCap = new AdjustableArrowCap(3, 3.5f);
                    else if (grafo.Dirigido) pen.CustomEndCap = new AdjustableArrowCap(5, 6);
                    g.DrawLine(pen, pa, pb);

                    if (hayPesos)
                    {
                        var m = new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                        g.DrawString(ar.Peso.ToString(), Font, Brushes.DarkBlue, m.X + 3, m.Y - 14);
                    }
                }
            }

            using (var centro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var fuente = new Font("Segoe UI", 10, FontStyle.Bold))
            {
                foreach (var v in grafo.Vertices)
                {
                    var p = pos[v];
                    g.FillEllipse(Brushes.LightSkyBlue, p.X - R, p.Y - R, 2 * R, 2 * R);
                    g.DrawEllipse(Pens.SteelBlue, p.X - R, p.Y - R, 2 * R, 2 * R);
                    g.DrawString(v, fuente, Brushes.Black, p, centro);
                }
            }
        }

        // ---------------- Dibujo de la red de actividades ----------------
        private void picRed_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            if (proyecto == null)
            {
                g.DrawString("Pulsa «Dibujar red y calcular caminos»", Font, Brushes.Gray, 20, 20);
                return;
            }

            var niveles = proyecto.Niveles();
            int maxN = niveles.Values.Max();
            var porNivel = niveles.GroupBy(kv => kv.Value)
                                  .ToDictionary(x => x.Key, x => x.Select(kv => kv.Key).ToList());
            float w = picRed.ClientSize.Width, h = picRed.ClientSize.Height;
            var pos = new Dictionary<string, PointF>();
            foreach (var par in porNivel)
            {
                int nivel = par.Key;
                var lista = par.Value;
                for (int i = 0; i < lista.Count; i++)
                    pos[lista[i]] = new PointF(
                        70 + (w - 140) * (maxN == 0 ? 0.5f : (float)nivel / maxN),
                        h * (i + 1) / (lista.Count + 1));
            }

            foreach (var act in proyecto.Orden)
                foreach (var pre in proyecto.Pred[act])
                {
                    bool crit = criticas.Contains(Tuple.Create(pre, act));
                    using (var pen = new Pen(crit ? Color.Crimson : Color.DimGray, crit ? 3.5f : 1.8f))
                    {
                        pen.CustomEndCap = new AdjustableArrowCap(5, 6);
                        var p1 = pos[pre]; var p2 = pos[act];
                        float dx = p2.X - p1.X, dy = p2.Y - p1.Y;
                        float len = (float)Math.Sqrt(dx * dx + dy * dy);
                        g.DrawLine(pen, p1.X + dx / len * 34, p1.Y + dy / len * 20,
                                        p2.X - dx / len * 38, p2.Y - dy / len * 22);
                    }
                }

            using (var centro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var fuente = new Font("Segoe UI", 10, FontStyle.Bold))
            {
                foreach (var act in proyecto.Orden)
                {
                    var p = pos[act];
                    var rect = new RectangleF(p.X - 32, p.Y - 18, 64, 36);
                    g.FillRectangle(Brushes.Khaki, rect);
                    g.DrawRectangle(Pens.DarkGoldenrod, rect.X, rect.Y, rect.Width, rect.Height);
                    g.DrawString(act + " (" + proyecto.Duracion[act] + ")", fuente, Brushes.Black, p, centro);
                }
            }
        }
    }
}