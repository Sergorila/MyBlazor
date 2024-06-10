using MyBlazor.DataAccess.Context;

namespace MyBlazor.DataAccess.Repo;

public class BaseRepo
{
    protected readonly MyContext myContext;

    protected BaseRepo(MyContext dbContext)
    {
        myContext = dbContext;
    }
}