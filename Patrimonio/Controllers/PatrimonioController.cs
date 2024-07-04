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
		public async Task<IActionResult> GetAsync(
			[FromServices] ApiContexto context)
		{
			var Itens = await context
				.Itens
				.AsNoTracking()
				.ToListAsync();
			return Ok(Itens);
		} 

		[HttpGet]
		[Route("TodosOsItens/{id}")]
		public async Task<IActionResult> GetByIdAsync(
			[FromServices] ApiContexto context,
			[FromRoute] int id)
		{
			var Itens = await context
				.Itens
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);

			return Itens == null
				? NotFound()
				: Ok(Itens);
		}

		[HttpPost("TodosOsItens")]
		public async Task<IActionResult> PostAsync(
		[FromServices] ApiContexto contexto,
		[FromBody] PatrimonioItens model)
		{
			if (!ModelState.IsValid)
				return BadRequest(model);

			var itens = new PatrimonioItens()
			{
				codigo_item = model.codigo_item,
				placa_item = model.placa_item,
				descricao_item = model.descricao_item,
				tipo_item = model.tipo_item,
				grupo_item = model.grupo_item,
				estado_conservacao = model.estado_conservacao,
				tipo_aquisicao = model.tipo_aquisicao,
				valor_aquisicao = model.valor_aquisicao,
				metodo_depreciacao = model.metodo_depreciacao,
				valor_residual = model.valor_residual,
				responsavel = model.responsavel,
				vida_util = model.vida_util,
				depreciacao_anual = model.depreciacao_anual,
				inicio_depreciacao = model.inicio_depreciacao,
				data_aquisicao = model.data_aquisicao,
				valor_depreciavel = model.valor_depreciavel,
				valor_depreciado = model.valor_depreciado,
				saldo_depreciar = model.saldo_depreciar,
				valor_liquido = model.valor_liquido,
				tipo_comprovante = model.tipo_comprovante,
				numero_comprovante = model.numero_comprovante,
				tem_combustivel = model.tem_combustivel,
				placa_veiculo = model.placa_veiculo,
				modelo_veiculo = model.modelo_veiculo,
				localizacao_fisica = model.localizacao_fisica,
				observacao = model.observacao,
				patrimonios_id = 1
			};

			try
			{
				await contexto.Itens.AddAsync(itens);
				await contexto.SaveChangesAsync();
				return Created($"v1/TodosOsItens/{itens.Id}", itens);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
		[HttpPut("TodosOsItens/{id}")]
		public async Task<IActionResult> PutAsync(
		[FromServices] ApiContexto contexto,
		[FromBody] PatrimonioItens model,
		[FromRoute] int id)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var itens = await contexto
				.Itens
				.FirstOrDefaultAsync(x => x.Id == id);

			if (itens == null)
			{
				return NotFound();
			}

			try
			{
				itens.codigo_item = model.codigo_item;
				itens.placa_item = model.placa_item;
				itens.descricao_item = model.descricao_item;
				itens.tipo_item = model.tipo_item;
				itens.grupo_item = model.grupo_item;
				itens.estado_conservacao = model.estado_conservacao;
				itens.tipo_aquisicao = model.tipo_aquisicao;
				itens.valor_aquisicao = model.valor_aquisicao;
				itens.metodo_depreciacao = model.metodo_depreciacao;
				itens.valor_residual = model.valor_residual;
				itens.responsavel = model.responsavel;
				itens.vida_util = model.vida_util;
				itens.depreciacao_anual = model.depreciacao_anual;
				itens.inicio_depreciacao = model.inicio_depreciacao;
				itens.data_aquisicao = model.data_aquisicao;
				itens.valor_depreciavel = model.valor_depreciavel;
				itens.valor_depreciado = model.valor_depreciado;
				itens.saldo_depreciar = model.saldo_depreciar;
				itens.valor_liquido = model.valor_liquido;
				itens.tipo_comprovante = model.tipo_comprovante;
				itens.numero_comprovante = model.numero_comprovante;
				itens.tem_combustivel = model.tem_combustivel;
				itens.placa_veiculo = model.placa_veiculo;
				itens.modelo_veiculo = model.modelo_veiculo;
				itens.localizacao_fisica = model.localizacao_fisica;
				itens.observacao = model.observacao;
				itens.patrimonios_id = 1;

				contexto.Itens.Update(itens);
				await contexto.SaveChangesAsync();
				return Ok(itens);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("TodosOsItens/{id}")]
		public async Task<IActionResult> DeleteAsync(
		[FromServices] ApiContexto contexto,
		[FromRoute] int id)
		{

			var itens = await contexto
				.Itens
				.FirstOrDefaultAsync(x => x.Id == id);

			try
			{
				contexto.Itens.Remove(itens);
				await contexto.SaveChangesAsync();
				return Ok();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpDelete("TodosOsItens")]
		public async Task<IActionResult> DeleteAsync(
					[FromServices] ApiContexto contexto,
					[FromBody] List<int> ids)
		{
			// Verifica se a lista de IDs é nula ou vazia
			if (ids == null || ids.Count == 0)
			{
				return BadRequest("A lista de IDs não pode estar vazia.");
			}

			// Busca os itens que correspondem aos IDs fornecidos
			var itens = await contexto.Itens
				.Where(x => ids.Contains(x.Id))
				.ToListAsync();

			// Verifica se todos os itens foram encontrados
			if (itens.Count != ids.Count)
			{
				return NotFound("Um ou mais itens não foram encontrados.");
			}

			try
			{
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
