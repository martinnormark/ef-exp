namespace EfExp.Models
{
	public interface IEntityFactory<TEntity> where TEntity : class, new()
	{
		TEntity Create();
		IEnumerable<TEntity> Create(int n);
	}
}