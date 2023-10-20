public class PickupFloatTrigger : PickupTrigger<float>
{
    protected override void Apply()
    {
       var newSourceValue = Source.RuntimeValue + ValueToAdd.InitialValue;
       
        if(Source.HasMax && newSourceValue > Source.MaxValue)
            Source.RuntimeValue = Source.MaxValue;
        else
            Source.RuntimeValue = newSourceValue;
    }
}