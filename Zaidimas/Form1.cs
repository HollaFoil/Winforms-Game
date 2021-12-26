using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zaidimas
{
    struct Shape
    {
        public int[,] grid;
        public int x, y;

        public Shape(int [,] g, int y, int x)
        {
            grid = g;
            this.x = x;
            this.y = y;
        }
        public bool inBounds(int m, int n, int size)
        {
            if (m + x > size) return false;
            if (n + y > size) return false;
            return true;
        }
    }
    public partial class Form1 : Form
    {
        private List<FlowLayoutPanel> collumns = new List<FlowLayoutPanel>();
        private List<Shape> shapes = new List<Shape>();
        private int[,] weights = null;
        private Random random = new Random();
        private List<SolutionClick> solution = new List<SolutionClick>();
        private int langeliuKiekis, displayedClick = 0;
        private int width, height;
        private bool answerVisible = false;
        private Stopwatch sw = new Stopwatch();
        public Form1()
        {
            langeliuKiekis = -1;
            InitializeComponent();
            width = GridFlexBox.Width;
            height = GridFlexBox.Height;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateShapes();
        }

        private void generuotiButton_Click(object sender, EventArgs e)
        {
            int timelimit;
            if (!int.TryParse(timeLimitTextBox.Text, out timelimit) || timelimit < 0)
            {
                MessageBox.Show("Laiko limitas netaisyklingas.");
                return;
            }
            sw.Restart();
            sw.Start();

            do
            {
                if (!int.TryParse(langeliuKiekisTextBox.Text, out langeliuKiekis) || langeliuKiekis < 2)
                {
                    MessageBox.Show("Langelių kiekis netaisyklingas.");
                    langeliuKiekis = -1;
                    sw.Reset();
                    return;
                }
                int simboliuKiekis;
                if (!int.TryParse(simboliuKiekisTextBox.Text, out simboliuKiekis) || simboliuKiekis < 1)
                {
                    MessageBox.Show("Simbolių kiekis netaisyklingas.");
                    simboliuKiekis = -1;
                    langeliuKiekis = -1;
                    sw.Reset();
                    return;
                }

                if (langeliuKiekis > 50)
                {
                    MessageBox.Show("Langelių kiekis per didelis.");
                    langeliuKiekis = -1;
                    sw.Reset();
                    return;
                }
                if (simboliuKiekis > 49)
                {
                    MessageBox.Show("Simbolių kiekis per didelis.");
                    simboliuKiekis = -1;
                    sw.Reset();
                    langeliuKiekis = -1;
                    return;
                }
                SuspendDrawing(GridFlexBox);
                helpLabel.ForeColor = Color.FromKnownColor(KnownColor.Control);
                coordsLabel.ForeColor = Color.FromKnownColor(KnownColor.Control);
                answerVisible = false;
                atsakymasButton.Text = "Rodyti atsakymą";
                collumns.Clear();
                GridFlexBox.Controls.Clear();
                GenerateNewGrid(langeliuKiekis, simboliuKiekis);
                FillOutGridCollumns(langeliuKiekis);
                simboliaiCheckBox_CheckedChanged(null, null);
                spalvosCheckBox_CheckedChanged(null, null);
                FitGridToBlocks(langeliuKiekis);
                GenerateWeightsFromGrid(langeliuKiekis);
                GameSolver gameSolver = new GameSolver(weights, langeliuKiekis, timelimit);
                if (!gameSolver.IsPossible() || !VerifyGrid(langeliuKiekis))
                {
                    //MessageBox.Show("Įmanomo žaidimo sugeneruoti nepavyko.\nSumažinkite simboliu kiekį arba bandykite vėl.\nTai galėjo nutikti per ilgai ieškant atsakymo ar jo neradus.", "ERROR 1");
                    ClearAndDisposeGrid();
                    langeliuKiekis = -1;
                    solution.Clear();
                    
                }
                displayedClick = 0;
                solution = gameSolver.GetSolution();
                ResumeDrawing(GridFlexBox);
                if (gameSolver.IsPossible()) return;
            } while (sw.ElapsedMilliseconds / 1000 < timelimit);
            MessageBox.Show("Įmanomo žaidimo sugeneruoti nepavyko.\nSumažinkite simboliu kiekį arba bandykite vėl.\nTai galėjo nutikti per ilgai ieškant atsakymo ar jo neradus.", "ERROR 2");
            ClearAndDisposeGrid();
            langeliuKiekis = -1;
            solution.Clear();
            sw.Reset();
        }
        private void GenerateWeightsFromGrid(int size)
        {
            weights = new int[size,size];
            int i = 0;
            foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
            {
                int j = 0;
                foreach (Label l in flp.Controls)
                {
                    weights[i, j++] = int.Parse(l.Name.Split(';')[2]);
                }
                i++;
            }
        }
        private void GenerateGridFromWeights(int size)
        {
            GridFlexBox.AutoSize = true;
            for (int i = 0; i < langeliuKiekis; i++) GridFlexBox.Controls.Add(GenerateFlowLayoutPanel(langeliuKiekis, i));
            foreach (FlowLayoutPanel flp in GridFlexBox.Controls) collumns.Add(flp);

            int uniqueID = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Label l = GenerateLabel(langeliuKiekis, weights[i,j], i, j, uniqueID++);
                    FlowLayoutPanel c = collumns[i];

                    c.Controls.Add(l);
                    c.Controls.SetChildIndex(l, j);
                }
            }
        }
        private void CreateShapes()
        {
            shapes.Add(new Shape(new int[,] { { 1, 1 } }, 2, 1));
            shapes.Add(new Shape(new int[,] { { 1 }, 
                                              { 1 } }, 1, 2));
            shapes.Add(new Shape(new int[,] { { 1, 1, 1 } }, 3, 1));
            shapes.Add(new Shape(new int[,] { { 1 }, 
                                              { 1 }, 
                                              { 1 } }, 1, 3));

            shapes.Add(new Shape(new int[,] { { 1, 0 }, 
                                              { 1, 1 } }, 2, 2));
            shapes.Add(new Shape(new int[,] { { 0, 1 },
                                              { 1, 1 } }, 2, 2));
            shapes.Add(new Shape(new int[,] { { 1, 1 },
                                              { 0, 1 } }, 2, 2));
            shapes.Add(new Shape(new int[,] { { 1, 1 },
                                              { 1, 0 } }, 2, 2));
        }
        private void FitGridToBlocks(int langeliuKiekis)
        {
            foreach (var flp in collumns) flp.Size = new Size(width/langeliuKiekis, height - (height % langeliuKiekis));
            GridFlexBox.AutoSize = false;
            


        }
        private void FillOutGridCollumns(int langeliuKiekis)
        {
            int max = Int32.MaxValue;
            foreach(FlowLayoutPanel flp in GridFlexBox.Controls)
            {
                if (flp.Controls.Count == langeliuKiekis) continue;
                int i = 0;
                Label top = null;
                foreach (Label l in flp.Controls)
                {
                    if (i++ != langeliuKiekis - 2) continue;
                    top = l; 
                }
                if (top == null) return;
                int x = int.Parse(top.Name.Split(';')[0]);
                int y = 50;
                int weight = int.Parse(top.Name.Split(';')[2]);
                flp.Controls.Add(GenerateLabel(langeliuKiekis, weight, x, y, max--));
            }
        }
        private void GenerateNewGrid(int langeliuKiekis, int simboliuKiekis)
        {
            GridFlexBox.AutoSize = true;
            for (int i = 0; i < langeliuKiekis; i++) GridFlexBox.Controls.Add(GenerateFlowLayoutPanel(langeliuKiekis, i));
            foreach (FlowLayoutPanel flp in GridFlexBox.Controls) collumns.Add(flp);
            

            int attempts = 0;
            int uniqueID = 0;
            while (attempts<langeliuKiekis*langeliuKiekis*langeliuKiekis)
            {
                attempts++;
                int x = random.Next(0, langeliuKiekis), y = random.Next(0, langeliuKiekis);
                random.Shuffle(shapes);
                Shape chosenShape = new Shape(new int[,]{ { } }, -1, -1);
                bool found = false;
                foreach (Shape shape in shapes)
                {
                    if (!FitsLocation(x, y, langeliuKiekis, shape)) continue;
                    chosenShape = shape;
                    found = true;
                    break;
                }
                if (!found) continue;
                attempts = 0;
                int weight = random.Next(0, simboliuKiekis);
                for (int i = 0; i < chosenShape.x; i++)
                {
                    for (int j = 0; j < chosenShape.y; j++)
                    {
                        Label l = GenerateLabel(langeliuKiekis, weight, x + i, y + j, uniqueID++);
                        FlowLayoutPanel c = collumns[i+x];
                        
                        c.Controls.Add(l);
                        c.Controls.SetChildIndex(l, y + j);
                    }
                }
            }
        }
        private bool FitsLocation(int x, int y, int size, Shape shape)
        {
            int m = shape.x, n = shape.y;
            if (!shape.inBounds(x, y, size)) return false;
            
            for (int i = 0; i < m; i++)
            {
                if (!(collumns[x+i].Controls.Count >= y)) return false;
                FlowLayoutPanel c = collumns[i+x];
                int sum = 0;
                if (c.Controls.Count + n > size) return false;
            }
            if (collumns[x].Controls.Count == 49 && m == 2 && n == 2)
            {
                x++;
            }
            return true;
        }
        private Label GenerateLabel(int size, int weight, int x, int y, int uniqueID)
        {
            
            Label square = new Label();
            square.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            square.Size = new System.Drawing.Size(width/size, height/size);
            square.TabIndex = 1;
            square.Name = x.ToString() + ";" + y.ToString() + ";" + weight.ToString() + ";" + uniqueID.ToString();
            square.Text = simboliai[weight];
            square.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            square.Margin = new System.Windows.Forms.Padding(0);
            square.Click += new System.EventHandler(this.label_Click);
            square.TextAlign = ContentAlignment.MiddleCenter;
            return square;
        }
        private FlowLayoutPanel GenerateFlowLayoutPanel(int size, int i)
        {
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Name = i.ToString();
            flp.Margin = new System.Windows.Forms.Padding(0);
            flp.Size = new System.Drawing.Size(width/size, height);
            flp.FlowDirection = FlowDirection.BottomUp;
            //flp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            return flp;
        }
        private Color GetColourFromRgb(string rgb)
        {
            int r = int.Parse(rgb[1].ToString() + rgb[2].ToString(), System.Globalization.NumberStyles.HexNumber);
            int g = int.Parse(rgb[3].ToString() + rgb[4].ToString(), System.Globalization.NumberStyles.HexNumber);
            int b = int.Parse(rgb[5].ToString() + rgb[6].ToString(), System.Globalization.NumberStyles.HexNumber);
            return Color.FromArgb(r, g, b);
        }
        private bool VerifyGrid(int langeliuKiekis)
        {
            foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
            {
                if (flp.Controls.Count != langeliuKiekis) return false;
            }
            return true;
        }
        

        private void simboliaiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (simboliaiCheckBox.Checked)
            {
                foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
                {
                    foreach (Label l in flp.Controls) l.Text = simboliai[int.Parse(l.Name.Split(';')[2])]; //l.Name;
                }
            }
            else
            {
                foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
                {
                    foreach (Label l in flp.Controls) l.Text = null;
                }
            }
        }

        private void spalvosCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SuspendDrawing(this);
            if (spalvosCheckBox.Checked)
            {
                foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
                {
                    foreach (Label l in flp.Controls)
                    {
                        l.BackColor = GetColourFromRgb(colourPallete[int.Parse(l.Name.Split(';')[2])]);
                        l.BorderStyle = BorderStyle.None;
                    }
                }
            }
            else
            {
                foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
                {
                    foreach (Label l in flp.Controls)
                    {
                        l.BackColor = Color.FromKnownColor(KnownColor.Control);
                        l.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }
            random.Shuffle(colourPallete);
            ResumeDrawing(this);
        }
        private void ClearAndDisposeGrid()
        {
            collumns.Clear();
            foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
            {
                foreach (Label l in flp.Controls)
                {
                    flp.Controls.Remove(l);
                    l.Dispose();
                }
                GridFlexBox.Controls.Remove(flp);
                flp.Dispose();
            }
            GridFlexBox.Controls.Clear();
            answerVisible = false;
            atsakymasButton.Text = "Rodyti atsakymą";
            helpLabel.ForeColor = Color.FromKnownColor(KnownColor.Control);
            coordsLabel.ForeColor = Color.FromKnownColor(KnownColor.Control);
            displayedClick = 0;
        }
        private void RemoveLabel(Label l)
        {
            int x = int.Parse(l.Name.Split(';')[0]);
            int y = int.Parse(l.Name.Split(';')[1]);
            FlowLayoutPanel flp = (FlowLayoutPanel)GridFlexBox.Controls.Find(x.ToString(), false)[0];
            Label t = (Label)flp.Controls.Find(l.Name, false)[0];
            flp.Controls.Remove(t);
            t.Dispose();
            if (flp.Controls.Count == 0)
            {
                GridFlexBox.Controls.Remove(flp);
                flp.Dispose();
            }
            
        }

        private void langeliuKiekisLabel_Click(object sender, EventArgs e)
        {

        }
        private List<Label> FindAdjacentLabels(List<Label> adj, Label l)
        {
            if (adj.Any(item => item.Name == l.Name)) return null;
            adj.Add(l);
            string weight = l.Name.Split(';')[2];
            Tuple<int, int> coords = FindLabelCoordinates(l);
            int x = coords.Item1;
            int y = coords.Item2;
            List<Label> next = new List<Label>();
            next.Add(FindLabelAtCoords(x + 1, y));
            next.Add(FindLabelAtCoords(x - 1, y));
            next.Add(FindLabelAtCoords(x, y + 1));
            next.Add(FindLabelAtCoords(x, y - 1));
            foreach (Label temp in next)
            {
                if (temp == null) continue;
                if (temp.Name.Split(';')[2] != weight) continue;
                FindAdjacentLabels(adj, temp);
            }
            return adj;
        }
        private Label FindLabelAtCoords(int x, int y)
        {
            int i = 0;
            foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
            {
                if (i++ != x) continue;
                int j = 0;
                foreach (Label l in flp.Controls)
                {
                    if (j++ != y) continue;
                    return l;
                }
            }
            return null;
        }
        private Tuple<int,int> FindLabelCoordinates(Label label)
        {
            int i = 0;
            foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
            {
                i++;
                int j = 0;
                foreach (Label l in flp.Controls)
                {
                    j++;
                    if (l == label) return new Tuple<int, int>(i-1, j-1);
                }
            }
            return null;
        }
        private void label_Click(object sender, EventArgs e)
        {
            List<Label> adjacent = new List<Label>();
            var coords = FindLabelCoordinates((Label)sender);
            FindAdjacentLabels(adjacent, (Label)sender);
            if (adjacent.Count < 2) return;
            foreach (Label l in adjacent) RemoveLabel(l);
            if (GridFlexBox.Controls.Count == 0)
            {
                MessageBox.Show("Pergalė!", "Žaidimas laimėtas");
                ClearAndDisposeGrid();
                return;
            }
            else if (coords.Item1+1 == solution[displayedClick].x && coords.Item2+1 == solution[displayedClick].y && answerVisible)
            {
                displayedClick++;
                coordsLabel.Text = "Spauskite:\n   x: " + solution[displayedClick].x.ToString() + "\n   y: " + solution[displayedClick].y.ToString();
            }
            else if (answerVisible)
            {
                answerVisible = false;
                atsakymasButton.Text = "Rodyti atsakymą";
                helpLabel.ForeColor = Color.FromKnownColor(KnownColor.Control);
                coordsLabel.ForeColor = Color.FromKnownColor(KnownColor.Control);
                MessageBox.Show("Klaidingas paspaudimas, atsakymas neberodomas.", "INFO");
            }
            foreach (FlowLayoutPanel flp in GridFlexBox.Controls)
            {
                foreach (Label l in flp.Controls) {
                    adjacent.Clear();
                    if (FindAdjacentLabels(adjacent, l).Count > 1) return;
                }
            }
            
            MessageBox.Show("Pralaimėjote...", "Žaidimas nebeįmanomas.");
            ClearAndDisposeGrid();
        }
        private const int WM_SETREDRAW = 11;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        public static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
        string[] simboliai = ("A B C D E F G H I J K L M N O P Q R S T U V W X Y Z Ą Č Ę Ė Į Š Ų Ū Ž " +
                              "a b c d e f g h i j k l m n o p q r s t u v w x y z ą č ę ė į š ų ū ž").Split(' ');

        private void atsakymasButton_Click(object sender, EventArgs e)
        {
            if (GridFlexBox.Controls.Count == 0) return;
            if (!VerifyGrid(langeliuKiekis) && !answerVisible)
            {
                MessageBox.Show("Prieš matant atsakymą, spauskite mygtuką \"Bandyti iš naujo\".", "ERROR");
                return;
            }
            if (answerVisible)
            {
                answerVisible = false;
                atsakymasButton.Text = "Rodyti atsakymą";
                helpLabel.ForeColor = Color.FromKnownColor(KnownColor.Control);
                coordsLabel.ForeColor = Color.FromKnownColor(KnownColor.Control);
                displayedClick = 0;
                return;
            }
            displayedClick = 0;
            atsakymasButton.Text = "Slėpti atsakymą";
            helpLabel.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            coordsLabel.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            coordsLabel.Text = "Spauskite:\n   x: " + solution[0].x.ToString() + "\n   y: " + solution[0].y.ToString();
            answerVisible = true;
        }

        private void retryButton_Click(object sender, EventArgs e)
        {
            if (langeliuKiekis == -1)
            {
                MessageBox.Show("Dar nepradėjote žaidimo.", "ERROR");
                return;
            }
            SuspendDrawing(GridFlexBox);
            ClearAndDisposeGrid();
            FillOutGridCollumns(langeliuKiekis);
            GenerateGridFromWeights(langeliuKiekis);
            simboliaiCheckBox_CheckedChanged(null, null);
            spalvosCheckBox_CheckedChanged(null, null);
            ResumeDrawing(GridFlexBox);
        }

        string[] colourPallete = { "#2f4f4f", "#556b2f", "#a0522d", "#2e8b57", "#7f0000", "#191970", "#708090",
            "#808000", "#008000", "#bc8f8f", "#4682b4", "#000080", "#d2691e", "#9acd32", "#20b2aa", "#32cd32", "#daa520",
            "#7f007f", "#8fbc8f", "#b03060", "#d2b48c", "#9932cc", "#ff0000", "#ff8c00", "#ffd700", "#ffff00", "#0000cd",
            "#00ff00", "#00ff7f", "#4169e1", "#dc143c", "#00ffff", "#00bfff", "#f4a460", "#0000ff", "#a020f0", "#adff2f",
            "#ff6347", "#da70d6", "#ff00ff", "#f0e68c", "#fa8072", "#6495ed", "#dda0dd", "#87ceeb", "#ff1493", "#98fb98", "#7fffd4", "#ff69b4"};
    }
}
