using IdentityService.Host.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Host.Data
{
    public  class UserDL
    {
        private string File { get; set; }

        //Clase que administra los archivos guardados
        private DataSource DataSource;
        private List<User> UsersList;
        private List<Persona> PersonasList;
        private string UsersData;
        private string PersonasData;

        public UserDL(string file)
        {
            this.File = file;
            this.DataSource = new DataSource(this.File);
        }

        private void Read()
        {
            //Leeo el archivo
            this.UsersData = this.DataSource.Read();
            //Convierto el archivo a una lista de personas, si es que tiene datos
            this.UsersList = this.UsersData?.Length > 0 ? JsonConvert.DeserializeObject<List<User>>(this.UsersData) : new List<User>();
        }

        private void Save()
        {
            //Convierto los datos a string 
            this.UsersData = JsonConvert.SerializeObject(this.UsersList);
            //guardo los datos en el archivo
            this.DataSource.Save(this.UsersData);
        }

        public int Save(User user)
        {
            Read();
            int id = 1;

            if (Exist(user.userId))
            {
                //Si existe solo actualizo
                var p = this.UsersList.First(x => x.userId == user.userId);
                p.email = user.email;
                p.password = user.password;
            }
            else
            {
                //Obtengo el id nuevo
                if (UsersList.Count > 0)
                {
                    id = this.UsersList.Max(x => x.userId) + 1;
                }
                user.userId = id;
                //Si no existte inserta uno nuevo
                this.UsersList.Add(user);
            }

            //Agrego la persona nueva a la lista de personas
            Save();
            return id;
        }

        public bool SavePersona(Persona persona)
        {
            this.PersonasData = this.DataSource.Read();
            this.PersonasList = this.PersonasData?.Length > 0 ? JsonConvert.DeserializeObject<List<Persona>>(this.PersonasData) : new List<Persona>();
            this.PersonasList.Add(persona);
            this.PersonasData = JsonConvert.SerializeObject(this.PersonasList);
            this.DataSource.Save(this.PersonasData);
            return true;
        }

        public bool Delete(int id)
        {
            this.PersonasData = this.DataSource.Read();
            this.PersonasList = this.PersonasData?.Length > 0 ? JsonConvert.DeserializeObject<List<Persona>>(this.PersonasData) : new List<Persona>();
            Persona persona = new Persona();
            if (PersonasList.Count > 0)
            {
                persona = this.PersonasList.FirstOrDefault(x => x.AnimoId == id);
            }
            if (persona.AnimoId > 0)
            {
                this.PersonasList.Remove(persona);
            }
            this.PersonasData = JsonConvert.SerializeObject(this.PersonasList);
            this.DataSource.Save(this.PersonasData);
            return true;
        }

        private bool Exist(int id)
        {
            User user = new User();
            if (UsersList.Count > 0)
            {
                user = this.UsersList.FirstOrDefault(x => x.userId == id);
            }
            return user?.userId > 0;
        }

        public List<User> Get()
        {
            Read();
            return this.UsersList;
        }

        public User Get(int id)
        {
            Read();
            User user = new User();
            if (UsersList.Count > 0)
            {
                user = this.UsersList.FirstOrDefault(x => x.userId == id);
            }
            return user;
        }

        public List<Persona> GetPersonas()
        {
            this.PersonasData = this.DataSource.Read();
            this.PersonasList = this.PersonasData?.Length > 0 ? JsonConvert.DeserializeObject<List<Persona>>(this.PersonasData) : new List<Persona>();
            return this.PersonasList;
        }
    }
}
