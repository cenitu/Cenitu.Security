namespace Cenitu.Security.Dtos.Unit
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Birim adı (kg, m, adet vb.)
        public string Symbol { get; set; } = string.Empty; // Sembol (kg, m, pcs vb.)
    }
}