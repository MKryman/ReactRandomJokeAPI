namespace Homework_06_05.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void NewUser(User user, string password)
        {
            var ctxt = new JokeContext(_connectionString);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = passwordHash;

            ctxt.Users.Add(user);
            ctxt.SaveChanges();

        }

        public User GetByEmail(string email)
        {
            var context = new JokeContext(_connectionString);
            if(email == null)
            {
                return null;
            }
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User LoginUser(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValid)
            {
                return null;
            }

            return user;
        }


    }
}
