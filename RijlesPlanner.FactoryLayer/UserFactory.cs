using RijlesPlanner.DataAccessLayer;
using RijlesPlanner.IDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RijlesPlanner.FactoryLayer
{
    public class UserFactory
    {
        public IUserContainerDal GetUserContainerDal()
        {
            return new UserDal();
        }
    }
}
