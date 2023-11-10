using MyApp.View;
using myTodo.Controller;

namespace myTodo.Model.Service;

public class UserController
{
    private TodoView _todoView;
    
    public void createUser(string name)
    {
        using (var db = new EFContext())
        {
            User user = new User(name);
            db.Add(user);
            db.SaveChanges();
            _todoView.display("User created");
        }
    }
}