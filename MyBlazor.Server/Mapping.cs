using AutoMapper;
using MyBlazor.DataAccess.Entities;
using MyBlazor.Server.Views;

namespace MyBlazor.Server;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UserView, User>();
        CreateMap<GameView, Game>();
        CreateMap<OrderView, Order>();
        CreateMap<CategoryView, Category>();
    }
    
}