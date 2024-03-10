using InscripcionesDapper.Entidades.Entidades;
using InscripcionesDapper.Entidades.Enum;
using InscripcionesDapper.Servicios.Interfaces;
using InscripcionesDapper.Servicios.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InscripcionesDapper.Windows.Helpers
{
    public static class CombosHelper
    {
        public static void CargarComboTurnos(ref ComboBox combo)
        {
            IServicioTurnos servicioTurnos = new ServicioTurnos();
            var lista = servicioTurnos.GetAll();
            var defaultPais = new Turno()
            {
                TurnoId = 0,
                DescripcionTurno = "Seleccione Turno"
            };
            lista.Insert(0, defaultPais);
            combo.DataSource = lista;
            combo.DisplayMember = "DescripcionTurno";
            combo.ValueMember = "TurnoId";
            combo.SelectedIndex = 0;
        }
        public static void CargarComboCarreras(ref ComboBox combo)
        {
            IServicioCarrera serviciosCarreras = new ServiciosCarreras();
            var lista = serviciosCarreras.GetAll();
            var defaultPais = new Carrera()
            {
                CarreraId = 0,
                NombreCarrera = "Seleccione Carrera"
            };
            lista.Insert(0, defaultPais);
            combo.DataSource = lista;
            combo.DisplayMember = "NombreCarrera";
            combo.ValueMember = "CarreraId";
            combo.SelectedIndex = 0;
        }

        public static void CargarComboGeneros(ref ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("Elija una opción");
            foreach (var genero in Enum.GetValues(typeof(Generos)))
            {
                    comboBox.Items.Add(genero.ToString());
            }
            comboBox.SelectedIndex = 0;
        }
        public static int ObtenerValorSeleccionado(ComboBox comboBox)
        {
            // Obtener el valor seleccionado del ComboBox
            string selectedValue = comboBox.SelectedItem.ToString();

            // Convertir el valor seleccionado a un entero basado en la enumeración
            if (Enum.TryParse(selectedValue, out Generos genero))
            {
                return (int)genero;
            }

            // Valor por defecto si no se puede convertir
            return 0;
        }

    }
}
