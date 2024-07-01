using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patrimonio.Modelos
{

	public class CreateItemDto
	{
		public Item ItemDetalhes { get; set; }
		public Aquisicao Aquisicao { get; set; }
		public Depreciacao Depreciacao { get; set; }
		public Localizacao Localizacao { get; set; }
		public Veiculo Veiculo { get; set; }
	}

	public class UpdateItemModel
	{
		public Item ItemDetalhes { get; set; }
		public Aquisicao Aquisicao { get; set; }
		public Depreciacao Depreciacao { get; set; }
		public Localizacao Localizacao { get; set; }
		public Veiculo Veiculo { get; set; }
	}


	[Table("Proj_Item")]
	public class Item
	{
		[Required]
		[Key]
		public string PlacaItem { get; set; } = "";
		[Required]
		public int CodigoItem { get; set; }
		[Required]
		public string DescricaoItem { get; set; } = "";
		[Required]
		public string TipoItem { get; set; } = "";
		[Required] 
		public string GrupoItem { get; set; } = "";
		[Required] 
		public string EstadoConservacao { get; set; } = "";
		public string Observacao { get; set; } = "";
		[Required] 
		public string Responsavel { get; set; } = "";

		[Required] 
		public Aquisicao? Aquisicao { get; set; }
		[Required] 
		public Depreciacao? Depreciacao { get; set; }
		public Localizacao? Localizacao { get; set; }
		public Veiculo? Veiculo { get; set; }
	}
	[Table("Proj_Aquisicao")]
	public class Aquisicao
	{
		[Required]
		[Key]
		public int idAquisicao { get; set; }
		[Required]
		public string TipoAquisicao { get; set; } = "";
		public string TipoComprovante { get; set; } = "";
		public string NumeroComprovante { get; set; } = "";
		[Required] 
		public DateTime DataAquisicao { get; set; }
		[Required] 
		public decimal ValorAquisicao { get; set; } = 0;
		[ForeignKey("Item")]
		public string ItemPlacaItem { get; set; } = "";
		[Required]
		Item? Item { get; set; }

	}
	[Table("Proj_Depreciacao")]
	public class Depreciacao
	{
		[Required]
		[Key]
		public int idDepreciacao { get; set; }
		[Required] 
		public string MetodoDepreciacao { get; set; } = "";
		[Required] 
		public decimal ValorResidual { get; set; }
		[Required] 
		public decimal ValorDepreciado { get; set; }
		[Required] 
		public int VidaUtil { get; set; }
		[Required] 
		public decimal DepreciacaoAnual { get; set; }
		[Required] 
		public DateTime DataInicioDepreciacao { get; set; }
		[Required] 
		public decimal SaldoDepreciar { get; set; }
		[Required] 
		public decimal ValorLiquido { get; set; }
		[Required] 
		public decimal ValorDepreciavel { get; set; }
		[ForeignKey("Item")]
		public string ItemPlacaItem { get; set; } = "";
		[Required]
		Item? Item { get; set; }
	}
	[Table("Proj_Localizacao")]
	public class Localizacao
	{
		[Required]
		[Key]
		public int idLocalizacao { get; set; }
		public string LocalizacaoFisica { get; set; } = "";
		[ForeignKey("Item")]
		public string ItemPlacaItem { get; set; } = "";
		[Required]
		Item? Item { get; set; }
	}
	[Table("Proj_Veiculos")]
	public class Veiculo
	{
		[Required]
		[Key]
		public int idVeiculos { get; set; }
		public string PlacaVeiculo { get; set; } = "";
		public string ModeloVeiculo { get; set; } = "";
		public bool UsaCombustivel { get; set; }
		[ForeignKey("Item")]
		public string ItemPlacaItem { get; set; } = "";
		[Required]
		Item? Item { get; set; }
	}

	[Table("Proj_HistoricoSaidaItem")]
	public class HistoricoSaida
	{
		[Required]
		[Key]
		public int idHistoricoSaidaItem { get; set; }
		[Required] 
		public DateTime DataSaida { get; set; }
		[Required] 
		public string MotivoSaida { get; set; } = "";
		[Required] 
		public string ResponsavelSaida { get; set; } = "";

		[ForeignKey("Item")]
		public string ItemPlacaItem { get; set; } = "";

		[Required] 
		Item? Item { get; set; }
	}
}
