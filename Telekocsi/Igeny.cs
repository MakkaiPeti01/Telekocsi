namespace Telekocsi
{
    internal class Igeny
    {
        public string Azonosito { get; private set; }
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public int Szemelyek { get; private set; }
        public Igeny(string sor)
        {
            string[] adatok = sor.Split(';');
            Azonosito = adatok[0];
            Indulas = adatok[1];
            Cel = adatok[2];
            Szemelyek = int.Parse(adatok[3]);
        }
    }
}