namespace Telekocsi
{
    internal class Autok
    {
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public string Rendszam { get; private set; }
        public string Telefonszam { get; private set; }
        public int Hely { get; private set; }
        public Autok(string sor)
        {
            string[] adatok = sor.Split(';');
            Indulas = adatok[0];
            Cel = adatok[1];
            Rendszam = adatok[2];
            Telefonszam = adatok[3];
            Hely = int.Parse(adatok[4]);
        }
    }
}