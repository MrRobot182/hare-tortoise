using System;
namespace testSerializacion
{
    [Serializable]
    public class Animal
    {
        private string nombre;
        private int velocidad;
        private bool seDuerme;

        public Animal() { }

        public Animal(string nombre, int velocidad, bool seDuerme)
        {
            this.nombre = nombre;
            this.velocidad = velocidad;
            this.seDuerme = seDuerme;
        }

        public override string ToString()
        {
            return string.Format("[Animal: nombre={0}, velocidad={1}, seDuerme={2}]", nombre, velocidad, seDuerme);
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public int Velocidad
        {
            get
            {
                return velocidad;
            }
            set
            {
                velocidad = value;
            }
        }

        public bool SeDuerme
        {
            get
            {
                return seDuerme;
            }
            set
            {
                seDuerme = value;
            }
        }
    }
}
