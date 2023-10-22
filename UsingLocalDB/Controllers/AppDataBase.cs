using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsingLocalDB.Models;

namespace UsingLocalDB.Controllers {
    public class AppDataBase {

        //DATABASE CONFIGURATION VARIABLES
        //=======================================================================================
        private readonly static string dbFileName = "myAppDataBase.db3";
        
        private readonly SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite |
                                                 SQLiteOpenFlags.Create |
                                                 SQLiteOpenFlags.SharedCache;

        private readonly string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);
        //---------------------------------------------------------------------------------------
        private SQLiteAsyncConnection connection;
        //======================================================================================



        public AppDataBase() {
            connection = new SQLiteAsyncConnection(dbPath, flags);
            connection.CreateTableAsync<Persona>().Wait();
        }







        public async Task<List<Persona>> SelectAll() {
            return await connection.Table<Persona>().ToListAsync();
        }


        public async Task<Persona> SelectById(int id) {
            return await connection.Table<Persona>().Where(col => col.Id == id).FirstOrDefaultAsync();
        }





        public async Task<int> Insert(Persona persona) {
            return await connection.InsertAsync(persona);
        }





        public async Task<int> Update(Persona persona) {
            return await connection.UpdateAsync(persona);
        }





        public async Task<int> Delete(Persona persona) {
            return await connection.DeleteAsync(persona);
        }



    }
}
