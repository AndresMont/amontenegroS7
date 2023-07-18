using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace amontenegroS7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        private SQLiteAsyncConnection con;
        public int IdSeleccionado;
        IEnumerable<Estudiante> rActualizar;
        IEnumerable<Estudiante> rEliminar;
        public Elemento(Estudiante datos)
        {
            InitializeComponent();
            txtNombre.Text = datos.Nombre;
            txtUsuario.Text = datos.Usuario;
            txtContrasena.Text = datos.Contrasena;
            IdSeleccionado = datos.Id;

            con = DependencyService.Get<Database>().GetConnection();

        }
        public static IEnumerable<Estudiante> Eliminar(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante where Id=?", id);

        }

        public static IEnumerable<Estudiante> Actualizar(SQLiteConnection db, int id, string nombre, string usuario, string contrasena)
        {
            return db.Query<Estudiante>("UPDATE Estudiante set Nombre=?,Usuario=?,Contrasena=? where Id=?", nombre, usuario, contrasena, id);

        }



        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                rActualizar = Actualizar(db, IdSeleccionado, txtNombre.Text, txtUsuario.Text, txtContrasena.Text);
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "Cerrar");

            }


        }

        private void btnELiminar_Clicked(object sender, EventArgs e)
        {

            try
            {
                var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(ruta);
                rEliminar = Eliminar(db, IdSeleccionado);
                Navigation.PushAsync(new ConsultaRegistro());

            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", ex.Message, "Cerrar");
            }

        }
    }
}