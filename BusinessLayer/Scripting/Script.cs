using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
//using Oss.BuisinessLayer.ViewDtos;
using System.Threading.Tasks;

namespace Oss.BusinessLayer.Scripting
{
    //class Script
    //{
    //    private ScriptState script = null;

    //    public void Reset()
    //    {
    //        script = null;
    //    }

    //    public void LoadFrom(DynamicObject obj)
    //    {
    //        var script = CSharpScript.Create("//Declarations");
    //        foreach (DynamicClassPropertyDefinition property in obj.Class.Properties)
    //        {
    //            script.ContinueWith($"{property.DataTypeSyntax.GetCSharpDataType()} {property.Name} = {property.DataTypeSyntax.GetCSharpLiteral(obj[property.Name])};");
    //        }
    //    }

    //    public async Task<T> Evaluate<T>(string expression)
    //    {
    //        return (await script.ContinueWithAsync<T>(expression)).ReturnValue;
    //    }
    //}
}
