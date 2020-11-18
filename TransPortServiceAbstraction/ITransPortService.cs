using System;
using System.Threading.Tasks;

namespace TransPortServiceAbstraction
{
    public interface ITransPortService
    {
        Task Send<T>(T data) where T : class;

 
    }
}
