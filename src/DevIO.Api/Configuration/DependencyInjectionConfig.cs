using DevIO.Business.Intefaces;
using DevIO.Business.Notificacoes;
using DevIO.Business.Services;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        #region Resolve Dependencias
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            #region Contexto
            services.AddScoped<MeuDbContext>();
            #endregion Contexto

            #region Repositorios
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            #endregion Repositorios

            #region Serviços
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<INotificador, Notificador>();
            #endregion Serviços

            return services;
        }
        #endregion Resolve Dependencias
    }
}
