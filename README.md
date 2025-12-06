# 🌾 **Mapa Vivo do Campo Inteligente – Sistema MVC de Gestão**  
Conectando o campo à tecnologia para uma agricultura mais eficiente e sustentável.  

---

## 🧰 **Tecnologias Utilizadas**  

### 🛠️ **Backend / MVC**  
- **ASP.NET Core MVC:** Estrutura principal do sistema para gerenciamento e renderização de páginas.  
- **Entity Framework Core:** Mapeamento ORM para acesso ao banco de dados SQL Server.  
- **SQL Server:** Banco de dados utilizado para armazenamento das informações.  
- **ASP.NET Identity (Opcional):** Controle de usuários e permissões.  
- **Automapper:** Conversão entre Models e ViewModels.  

### 🎨 **Frontend (Razor + Bootstrap)**  
- **Razor Views:** Renderização dinâmica de páginas com C# + HTML.  
- **Bootstrap 5:** Layout responsivo e componentes visuais.  
- **Chart.js:** Gráficos interativos para dashboards.  
- **jQuery:** Suporte para interações dinâmicas e AJAX.  

---

## 📑 **Índice**  
- **Visão Geral**  
- **Por que usar o Mapa Vivo do Campo Inteligente – MVC?**  
- **Começando**  
- **Pré-requisitos**  
- **Instalação**  
- **Como Usar**  
- **APIs e Integração IoT**  
- **Testes**  

---

## 🔍 **Visão Geral**  
**Mapa Vivo do Campo Inteligente – MVC** é uma plataforma moderna e escalável voltada para o **gerenciamento e monitoramento agrícola em tempo real**.  
O sistema integra sensores **IoT** e **microcontroladores** para coletar dados ambientais como temperatura, umidade, luz e qualidade do ar, exibindo informações por meio de um **painel administrativo interativo**.  
A solução permite **democratizar o acesso à agricultura de precisão**, oferecendo tecnologia acessível e sustentável para produtores de diferentes portes.  

---

## 🌱 **Por que usar o Mapa Vivo do Campo Inteligente – MVC?**  
Este projeto foi desenvolvido para **otimizar a gestão agrícola**, permitindo que produtores e cooperativas tomem decisões mais assertivas com base em dados confiáveis do campo.  

Os principais recursos incluem:  
- 🔧 **Integração IoT:** Sensores de solo, luz e temperatura conectados a microcontroladores como ESP32 e Arduino.  
- 📊 **Painel Administrativo Interativo:** Visualização de dados ambientais e históricos em gráficos e tabelas.  
- ⚙️ **Automação e Alertas:** Notificações automáticas sobre condições críticas de clima e umidade.  
- 🌐 **APIs RESTful:** Comunicação eficiente entre dispositivos IoT e o backend MVC.  
- 💾 **Armazenamento de Dados:** Registro histórico para análise de desempenho agrícola.  
- 📱 **Interface Responsiva:** Layout adaptável para desktop, tablets e dispositivos móveis.  
- 🚀 **Escalabilidade:** Estrutura pronta para expansão em novas regiões e culturas agrícolas.  

---

## 🚀 **Começando**  

### ✅ **Pré-requisitos**  
Antes de iniciar, verifique se você possui:  
- **SDK .NET 8**  
- **Visual Studio 2022 ou VS Code**  
- **SQL Server instalado**  

---

## 🛠️ **Instalação**  
Siga os passos abaixo para configurar o projeto em sua máquina local:  


git clone https://github.com/italonensai/mapa-vivo-campo-inteligente-mvc
cd mapa-vivo-campo-inteligente-mvc
dotnet restore
dotnet ef database update
dotnet run

## 🛠️ **Desenvolvido por:**

- Emanuelly Vitoria dos Santos Lima
- Ítalo Francesco
- Rayssa Nanclares da Silveira

**Documentação:**
[DOC TECNICA MVC - TRISTACK.pdf](https://github.com/user-attachments/files/23996865/DOC.TECNICA.MVC.-.TRISTACK.pdf)
