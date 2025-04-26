using iiwi.Model;


        public static class Authentication
        {
            public static EndpointDetails Group =>  new()
            {
                Name = "Authentication",
                Tags = "Authentication",
                Summary = "Authentication",
                Description = "This group contains all the endpoints related to authentication."
            };
        }