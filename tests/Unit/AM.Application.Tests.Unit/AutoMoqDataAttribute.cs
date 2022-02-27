using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace AM.Application.Tests.Unit;

public class AutoMoqDataAttribute:AutoDataAttribute
{
    
    public  AutoMoqDataAttribute():base(()=>new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}