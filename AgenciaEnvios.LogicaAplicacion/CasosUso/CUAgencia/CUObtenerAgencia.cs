using AgenciaEnvios.DTOs.DTOs.DTOAgencia;
using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUAgencia;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUAgencia
{
    public class CUObtenerAgencia : ICUObtenerAgencia
    {
        private  IRepositorioAgencia _repositorioAgencia;

        public CUObtenerAgencia(IRepositorioAgencia repositorioAgencia)
        {
            _repositorioAgencia = repositorioAgencia;
        }

       

        public List<DTOAgencia> ObtenerAgencias()
        {
            List<Agencia> agencias = _repositorioAgencia.FindAll();
            return MapperAgencia.FromListAgenciaToListDtoAgencia(agencias);
        }
    }


}
