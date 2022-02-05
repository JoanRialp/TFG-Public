using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TFG_Web.Models
{
    public class Usuarios
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string P_Username { get; set; }
        public string P_Correo { get; set; }
        public string P_Password { get; set; }
        public int P_Grupo { get; set; }
        public DateTime P_Fecha_Inicio { get; set; }

        /// <summary>
        /// Encriptar la contraseña del usuario (Login/Register)
        /// </summary>
        /// <param name="userPasswsord"></param>
        /// <returns></returns>
        public string cryptedPassword(string userPasswsord)
        {
            byte[] salt = new byte[128 / 8];
            string hashPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: userPasswsord,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            return hashPassword;
        }
    }
}