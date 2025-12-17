
public interface IBindable<T>
{
    public void Bind(T data);

    public void SetState(bool isSelected, bool isFocused);
}
