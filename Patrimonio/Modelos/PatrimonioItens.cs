using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patrimonio.Modelos
{
	[Table("itens")]
	public class PatrimonioItens
	{
		public int Id { get; set; }

		[Required]
		public string? codigo_item { get; set; } = "";

		[Required]
		public string? placa_item { get; set; } = "";

		[Required]
		public string? descricao_item { get; set; } = "";

		[Required]
		public string? tipo_item { get; set; } = "";

		[Required]
		public string? grupo_item { get; set; } = "";

		[Required]
		public string? estado_conservacao { get; set; } = "";

		[Required]
		public string? tipo_aquisicao { get; set; } = "";

		[Required]
		public string? valor_aquisicao { get; set; } = "";

		[Required]
		public string? metodo_depreciacao { get; set; } = "";

		[Required]
		public string? valor_residual { get; set; } = "";

		[Required]
		public string? responsavel { get; set; } = "";

		[Required]
		public string? vida_util { get; set; } = "";

		[Required]
		public string? depreciacao_anual { get; set; } = "";

		[Required]
		public DateTime inicio_depreciacao { get; set; }

		[Required]
		public DateTime data_aquisicao { get; set; }

		[Required]
		public string? valor_depreciavel { get; set; } = "";

		[Required]
		public string? valor_depreciado { get; set; } = "";

		[Required]
		public string? saldo_depreciar { get; set; } = "";

		[Required]
		public string? valor_liquido { get; set; } = null;

		public string? tipo_comprovante { get; set; } = null;

		public string? numero_comprovante { get; set; } = null;

		public string? tem_combustivel { get; set; } = null;

		public string? placa_veiculo { get; set; } = null;

		public string? modelo_veiculo { get; set; } = null;

		public string? localizacao_fisica { get; set; } = null;

		public string? observacao { get; set; } = null;

		[Required]
		public int patrimonios_id { get; set; } = 1;
	}
}
