using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPATEC_Service_2.Models
{

    /// <summary>
    /// Clase que representa a la tabla USUARIO en la base de datos
    /// </summary>
    public class Usuario
    {

        public string _usuario { get; set; }
        public string _contrasena { get; set; }
        public Int64 _cedula { get; set; }
        public string _nombre { get; set; }
        public string _pApellido { get; set; }
        public string _sApellido { get; set; }
        public DateTime _fNacimiento { get; set; }
        public string _telefono { get; set; }
        public string _provincia { get; set; }
        public string _canton { get; set; }
        public string _distrito { get; set;}
        public string _rol { get; set; }
        
    }
}