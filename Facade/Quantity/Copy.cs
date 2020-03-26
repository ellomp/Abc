namespace Abc.Facade.Quantity
{
    public static class Copy
    {
        public static void Members(object from, object to)
        {
            foreach (var property in from.GetType().GetProperties())
            {
                var name = property.Name;
                var p = to.GetType().GetProperty(name);
                var v = property.GetValue(from); //küsin property väärtuse objecti "from" joaks
                p.SetValue(to, v);
            }
        }
    }
}