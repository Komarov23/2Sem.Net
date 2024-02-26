using System.Reflection;

public class MyClass
{
    private int privateField;
    public string publicField;
    protected float protectedField;
    internal double internalField;
    public static bool staticField;

    public void Method1()
    {
        Console.WriteLine("Method1 called.");
    }

    public int Method2(int param1, string param2)
    {
        Console.WriteLine($"Method2 called with parameters: {param1}, {param2}");
        return param1 + param2.Length;
    }

    protected void Method3()
    {
        Console.WriteLine("Method3 called.");
    }
}

class Program
{
    static void Main()
    {
        Type myClassType = typeof(MyClass);
        object myClassInstance = Activator.CreateInstance(myClassType);

        TypeInfo myClassTypeInfo = myClassType.GetTypeInfo();
        Console.WriteLine($"Class Name: {myClassType.Name}");
        Console.WriteLine($"Is Class Abstract: {myClassTypeInfo.IsAbstract}");
        Console.WriteLine($"Is Class Sealed: {myClassTypeInfo.IsSealed}");

        MemberInfo[] members = myClassType.GetMembers();
        Console.WriteLine("\nMembers:");
        foreach (var member in members)
        {
            Console.WriteLine($"  {member.Name}, MemberType: {member.MemberType}");
        }

        FieldInfo[] fields = myClassType.GetFields();
        Console.WriteLine("\nFields:");
        foreach (var field in fields)
        {
            Console.WriteLine($"  {field.Name}, FieldType: {field.FieldType}");
        }

        MethodInfo methodInfo = myClassType.GetMethod("Method1");
        Console.WriteLine($"\nCalling Method1 using Reflection:");
        methodInfo.Invoke(myClassInstance, null);

        methodInfo = myClassType.GetMethod("Method2");
        Console.WriteLine($"\nCalling Method2 using Reflection:");
        object[] parameters = { 42, "Hello" };
        int result = (int)methodInfo.Invoke(myClassInstance, parameters);
        Console.WriteLine($"Method2 returned: {result}");
    }
}