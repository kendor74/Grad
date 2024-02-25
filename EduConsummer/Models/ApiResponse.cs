namespace EduConsummer.Models
{
    public class ApiResponse<T> : IApiRequest<T> where T : class
    {
        private string url = "http://localhost:5000";
        HttpClient client = new HttpClient();

        public ApiResponse()
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        public Task<T> Delete(string api, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll(string api)
        {
            try
            {
                HttpResponseMessage Res = await client.GetAsync(api);
                List<T>? list = new List<T>();
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponcse = Res.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<T>>(EmpResponcse);
                }
                return list;

            }
            catch(Exception ex) { }
            {
                return new List<T>();
            }

        }

        public Task<T> GetByEmail(string api, string Email)
        {

            //thinking on how to send Email to get the Data to be displied in the User Profile
            throw new NotImplementedException();
        }

        public async Task<T> GetById(string api, int id)
        {
            HttpResponseMessage Res = await client.GetAsync(api);
            if (Res.IsSuccessStatusCode)
            {
                var result = await Res.Content.ReadAsAsync<T>();
                return result;
            }

            return null;
        }

        public async Task<string> Login(string Email , string Password)
        {

            var user = new { Email = Email, Password = Password };
            HttpResponseMessage Res = await client.PostAsJsonAsync("api/User/Login", user);

            Res.EnsureSuccessStatusCode();
            
            return user.ToString();
        }

        public async Task<string> Logout()
        {
            HttpResponseMessage Res = await client.PostAsync("api/User/Logout",null);

            Res.EnsureSuccessStatusCode();
            
            return Res.Content.ReadAsStringAsync().ToString();
        }

        public async Task<T> Post(string api, T entity)
        {
            HttpResponseMessage Res = await client.PostAsJsonAsync(api, entity);


            Res.EnsureSuccessStatusCode();
            entity = await Res.Content.ReadAsAsync<T>();
            return entity;
        }

        public async Task<T> Put(string api, T entity)
        {
            HttpResponseMessage Res = await client.PutAsJsonAsync(api, entity);

            Res.EnsureSuccessStatusCode();
            entity = await Res.Content.ReadAsAsync<T>();
            return entity;
        }

        public Task<string> SigUp(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        
    }
}
