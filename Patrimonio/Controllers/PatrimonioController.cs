using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Patrimonio.Data;
using Patrimonio.Modelos;

namespace Patrimonio.Controllers
{
	[ApiController]
	[Route("v1")]
	public class PatrimonioController : ControllerBase
	{
		public readonly ApiContexto _context;

		public PatrimonioController(ApiContexto context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("TodosOsItens")]
		[EnableCors("AllowSpecificOrigin")]
		public async Task<IActionResult> GetAsync(
			[FromServices] ApiContexto context)
		{
			var itens = await context
				.Itens
				.AsNoTracking()
				.Include(i => i.Aquisicao)
				.Include(i => i.Depreciacao)
				.Include(i => i.Localizacao)
				.Include(i => i.Veiculo)
				.ToListAsync();

			return Ok(itens);
		}

		[HttpGet]
		[Route("TodosOsItens/{placaItem}")]
		[EnableCors("AllowSpecificOrigin")]
		public async Task<IActionResult> GetByIdAsync(
			[FromServices] ApiContexto context,
			[FromRoute] string placaItem)
		{
			var item = await context
				.Itens
				.AsNoTracking()
				.Include(i => i.Aquisicao)
				.Include(i => i.Depreciacao)
				.Include(i => i.Localizacao)
				.Include(i => i.Veiculo)
				.FirstOrDefaultAsync(x => x.PlacaItem == placaItem);

			return item == null ? NotFound() : Ok(item);
		}

		[HttpPost("TodosOsItens")]
		public async Task<IActionResult> PostAsync(
			[FromServices] ApiContexto contexto,
			[FromBody] CreateItemDto dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var itens = new Item
			{
				PlacaItem = dto.ItemDetalhes.PlacaItem,
				CodigoItem = dto.ItemDetalhes.CodigoItem,
				DescricaoItem = dto.ItemDetalhes.DescricaoItem,
				TipoItem = dto.ItemDetalhes.TipoItem,
				GrupoItem = dto.ItemDetalhes.GrupoItem,
				EstadoConservacao = dto.ItemDetalhes.EstadoConservacao,
				Responsavel = dto.ItemDetalhes.Responsavel,
				Observacao = dto.ItemDetalhes.Observacao,
				Aquisicao = new Aquisicao
				{
					TipoAquisicao = dto.Aquisicao.TipoAquisicao,
					TipoComprovante = dto.Aquisicao.TipoComprovante,
					NumeroComprovante = dto.Aquisicao.NumeroComprovante,
					ValorAquisicao = dto.Aquisicao.ValorAquisicao,
					DataAquisicao = dto.Aquisicao.DataAquisicao
				},
				Depreciacao = new Depreciacao
				{
					MetodoDepreciacao = dto.Depreciacao.MetodoDepreciacao,
					ValorResidual = dto.Depreciacao.ValorResidual,
					ValorDepreciado = dto.Depreciacao.ValorDepreciado,
					VidaUtil = dto.Depreciacao.VidaUtil,
					DepreciacaoAnual = dto.Depreciacao.DepreciacaoAnual,
					DataInicioDepreciacao = dto.Depreciacao.DataInicioDepreciacao,
					SaldoDepreciar = dto.Depreciacao.SaldoDepreciar,
					ValorLiquido = dto.Depreciacao.ValorLiquido,
					ValorDepreciavel = dto.Depreciacao.ValorDepreciavel
				},
				Localizacao = new Localizacao
				{
					LocalizacaoFisica = dto.Localizacao.LocalizacaoFisica
				},
				Veiculo = new Veiculo
				{
					PlacaVeiculo = dto.Veiculo.PlacaVeiculo,
					ModeloVeiculo = dto.Veiculo.ModeloVeiculo,
					UsaCombustivel = dto.Veiculo.UsaCombustivel
				}
			};

			try
			{
				await contexto.Itens.AddAsync(itens);
				await contexto.SaveChangesAsync();
				return Created($"v1/TodosOsItens/{itens.PlacaItem}", itens);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			
		}

		[HttpPut("TodosOsItens/{id}")]
		public async Task<IActionResult> PutAsync(
			[FromServices] ApiContexto contexto,
			[FromBody] UpdateItemModel updateModel,
			[FromRoute] string id)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var itens = await contexto
				.Itens
				.Include(i => i.Aquisicao)
				.Include(i => i.Depreciacao)
				.Include(i => i.Localizacao)
				.Include(i => i.Veiculo)
				.FirstOrDefaultAsync(x => x.PlacaItem == id);

			if (itens == null)
			{
				return NotFound();
			}

			try
			{
				// Atualizar propriedades de Item
				itens.CodigoItem = updateModel.ItemDetalhes.CodigoItem;
				itens.DescricaoItem = updateModel.ItemDetalhes.DescricaoItem;
				itens.TipoItem = updateModel.ItemDetalhes.TipoItem;
				itens.GrupoItem = updateModel.ItemDetalhes.GrupoItem;
				itens.EstadoConservacao = updateModel.ItemDetalhes.EstadoConservacao;
				itens.Responsavel = updateModel.ItemDetalhes.Responsavel;
				itens.Observacao = updateModel.ItemDetalhes.Observacao;

				// Atualizar propriedades de Aquisicao
				if (itens.Aquisicao != null && updateModel.Aquisicao != null)
				{
					itens.Aquisicao.TipoAquisicao = updateModel.Aquisicao.TipoAquisicao;
					itens.Aquisicao.TipoComprovante = updateModel.Aquisicao.TipoComprovante;
					itens.Aquisicao.NumeroComprovante = updateModel.Aquisicao.NumeroComprovante;
					itens.Aquisicao.ValorAquisicao = updateModel.Aquisicao.ValorAquisicao;
					itens.Aquisicao.DataAquisicao = updateModel.Aquisicao.DataAquisicao;
				}

				// Atualizar propriedades de Depreciacao
				if (itens.Depreciacao != null && updateModel.Depreciacao != null)
				{
					itens.Depreciacao.MetodoDepreciacao = updateModel.Depreciacao.MetodoDepreciacao;
					itens.Depreciacao.ValorResidual = updateModel.Depreciacao.ValorResidual;
					itens.Depreciacao.ValorDepreciado = updateModel.Depreciacao.ValorDepreciado;
					itens.Depreciacao.VidaUtil = updateModel.Depreciacao.VidaUtil;
					itens.Depreciacao.DepreciacaoAnual = updateModel.Depreciacao.DepreciacaoAnual;
					itens.Depreciacao.DataInicioDepreciacao = updateModel.Depreciacao.DataInicioDepreciacao;
					itens.Depreciacao.SaldoDepreciar = updateModel.Depreciacao.SaldoDepreciar;
					itens.Depreciacao.ValorLiquido = updateModel.Depreciacao.ValorLiquido;
					itens.Depreciacao.ValorDepreciavel = updateModel.Depreciacao.ValorDepreciavel;
				}

				// Atualizar propriedades de Localizacao
				if (itens.Localizacao != null && updateModel.Localizacao != null)
				{
					itens.Localizacao.LocalizacaoFisica = updateModel.Localizacao.LocalizacaoFisica;
				}

				// Atualizar propriedades de Veiculo
				if (itens.Veiculo != null && updateModel.Veiculo != null)
				{
					itens.Veiculo.PlacaVeiculo = updateModel.Veiculo.PlacaVeiculo;
					itens.Veiculo.ModeloVeiculo = updateModel.Veiculo.ModeloVeiculo;
					itens.Veiculo.UsaCombustivel = updateModel.Veiculo.UsaCombustivel;
				}

				contexto.Itens.Update(itens);
				await contexto.SaveChangesAsync();

				return Ok(itens);
			}
			catch (DbUpdateException ex)
			{
				// Handle the exception appropriately
				return BadRequest("Erro ao salvar as alterações no banco de dados.");
			}

		}


		[HttpDelete("TodosOsItens/{id}")]
		public async Task<IActionResult> DeleteAsync(
			[FromServices] ApiContexto contexto,
			[FromRoute] string id)
		{
			var item = await contexto
				.Itens
				.Include(i => i.Aquisicao)
				.Include(i => i.Depreciacao)
				.Include(i => i.Localizacao)
				.Include(i => i.Veiculo)
				.FirstOrDefaultAsync(x => x.PlacaItem == id);

			if (item == null)
			{
				return NotFound();
			}

			try
			{
				contexto.Itens.Remove(item);
				await contexto.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpDelete("TodosOsItens")]
		public async Task<IActionResult> DeleteAsync(
	[FromServices] ApiContexto contexto,
	[FromBody] List<string> placasItens)
		{
			// Verifica se a lista de placas de itens é nula ou vazia
			if (placasItens == null || placasItens.Count == 0)
			{
				return BadRequest("A lista de placas de itens não pode estar vazia.");
			}

			try
			{
				// Busca os itens que correspondem às placas fornecidas
				var itens = await contexto.Itens
					.Where(x => placasItens.Contains(x.PlacaItem))
					.ToListAsync();

				// Verifica se todos os itens foram encontrados
				if (itens.Count != placasItens.Count)
				{
					// Identifica quais placas de itens não foram encontradas
					var placasNaoEncontradas = placasItens.Except(itens.Select(i => i.PlacaItem));
					return NotFound($"Um ou mais itens não foram encontrados: {string.Join(", ", placasNaoEncontradas)}");
				}

				// Remove todos os itens encontrados
				contexto.Itens.RemoveRange(itens);
				await contexto.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				// Em caso de erro, retorna um status de BadRequest
				return BadRequest(ex.Message);
			}
		}
	}
}
