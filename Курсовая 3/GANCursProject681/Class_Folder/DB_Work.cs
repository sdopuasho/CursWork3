using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GANCursProject681.Class_Folder
{
    class DB_Work
    {
        ANDBEntities db = new ANDBEntities();
        public int returnid(string login, string password)
        {
            return db.Workers.Where(w => w.Login == login && w.Password == password).Select(s => s.id).FirstOrDefault();
        }
        public int returnidlog(string login)
        {
            return db.Workers.Where(w => w.Login == login).Select(s => s.id).FirstOrDefault();
        }
        public int ReturnlvlAccess(int idUser)
        {
            return db.Workers.Include(i => i.Role).Where(w => w.id == idUser).Select(s => s.Role.LevelOfAccess).FirstOrDefault();
        }
        public int ReturnidRole(string pos, int lvl)
        {
            return db.Role.Where(w => w.Name == pos && w.LevelOfAccess == lvl).Select(s => s.id).FirstOrDefault();
        }
        public int ReturnidRoleForRegistration(string pos)
        {
            return db.Role.Where(w => w.Name == pos).Select(s => s.id).FirstOrDefault();
        }

        public int ReturnidForFullInfo(string fnm, string lnm, string mnm, string phn)
        {
            return db.Workers.Where(w => w.FirstName == fnm && w.LastName == lnm && w.MiddleName == mnm && w.ContactNumber == phn).Select(s => s.id).FirstOrDefault();
        }
    }
}
