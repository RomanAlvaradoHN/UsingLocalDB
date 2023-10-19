using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UsingLocalDB.Models {
    public class Persona {
        private List<string> invalidData = new List<string>();

        private string nombre;
        private string apellido;
        private string dni;

        public Persona() {
        }

        public Persona(string nombre, string apellido, string dni) {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Dni = dni;
        }


        public List<string> GetDatosInvalidos() {
            return this.invalidData;
        }




        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }




        [Indexed]
        [Column("Dni")]
        public string Dni {
            get { return this.dni; }

            set {
                if (!string.IsNullOrEmpty(value)) {
                    this.dni = value;
                } else {
                    this.invalidData.Add("DNI");
                }
            }
        }




        [Column("Nombre")]
        public string Nombre {
            get { return this.nombre;}

            set {
                string regExpPattern = "^[a-zA-Z' -]+$";

                if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, regExpPattern)) {
                    this.nombre = value;
                } else {
                    this.invalidData.Add("Nombre");
                }
            }
        }


        [Column("Apellido")]
        public string Apellido {
            get { return this.apellido; }

            set {
                string regExpPattern = "^[a-zA-Z' -]+$";

                if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, regExpPattern)) {
                    this.apellido = value;
                } else {
                    this.invalidData.Add("Apellido");
                }
            }
        }

    }
}
