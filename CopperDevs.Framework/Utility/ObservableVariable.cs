namespace CopperDevs.Framework.Utility;

// inspiration https://gist.github.com/vantreeseba/c19c2387bac86047f30d7bec1e41a824
public class ObservableVariable<T>
{
    private T internalValue;

    public ObservableVariable(T internalValue)
    {
        this.internalValue = internalValue;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ObservableVariable()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public T Value
    {
        get => internalValue;
        set
        {
            if (internalValue != null && internalValue.Equals(value))
                return;

            internalValue = value;
            OnChange?.Invoke(internalValue);
        }
    }

    public event Action<T> OnChange = null!;
    public static implicit operator T(ObservableVariable<T> obs) => obs.internalValue;
    public override string ToString() => internalValue?.ToString() ?? string.Empty;
}