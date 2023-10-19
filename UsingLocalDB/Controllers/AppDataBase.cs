using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsingLocalDB.Models;

namespace UsingLocalDB.Controllers {
    public class AppDataBase {

        //DataBase configuration variables
        //=======================================================================================
        SQLiteAsyncConnection connection;
        
        private const string dbFileName = "myAppData.db3";

        private const SQLiteOpenFlags flags = 
            SQLiteOpenFlags.ReadWrite | 
            SQLiteOpenFlags.Create | 
            SQLiteOpenFlags.SharedCache;

        private static readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);
        //======================================================================================



        public AppDataBase() { }







        public async Task Init() {
            if (connection is not null) {
                return;

            } else {
                connection = new SQLiteAsyncConnection(dbPath, flags);
                var result = await connection.CreateTableAsync<Persona>();
            }
        }




        public async Task<List<Persona>> SelectAll() {
            await Init();
            return await connection.Table<Persona>().ToListAsync();
        }




        public async Task<Persona> SelectById(int id) {
            await Init();
            return await connection.Table<Persona>().Where(col => col.Id == id).FirstOrDefaultAsync();
        }



        public async Task<int> CreateOrUpdate(Persona persona) {
            await Init();
            if(persona.Id != 0) {
                return await connection.UpdateAsync(persona);

            } else {
                return await connection.InsertAsync(persona);
            }
        }



        public async Task<int> Delete(Persona persona) {
            await Init();
            return await connection.DeleteAsync(persona);
        }



    }
}
