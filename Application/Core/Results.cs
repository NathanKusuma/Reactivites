namespace Application.Core
{
    public class Results<T> //Using Generic type to use any of entitites
    {

        public bool IsSuccess { get; set; }

        public T Value  { get; set; }

        public string Error {get; set;}

        public static Results<T> Success(T value)=> new Results<T>{IsSuccess=true,Value=value};
        
        public static Results<T> Failure(string error)=> new Results<T>{IsSuccess=false,Error=error};
    }
}