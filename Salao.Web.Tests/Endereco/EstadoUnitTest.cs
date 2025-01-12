﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Service.Endereco;

namespace Salao.Web.Tests.Endereco
{
    [TestClass]
    public class EstadoUnitTest
    {
        EstadoService service;

        public EstadoUnitTest()
        {
            service = new EstadoService();
        }

        [TestMethod]
        public void EstadoIncluir()
        {
            // Arrange
            var estado = new EnderecoEstado { Descricao = "SAO PAULO", UF = "SP" };
            
            // Act
            int id = service.Gravar(estado);
            string uf = service.Find(1).UF;

            // Assert
            Assert.AreEqual("SP", uf);
        }
    }
}
