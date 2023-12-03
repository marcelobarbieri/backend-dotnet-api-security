<h1>Segurança em APIs ASP.NET com JWT e Bearer Authentication</h1>

Ref.: Balta.io

> O conteúdo também foi organizado nos **commits**

<!--#region Sumário -->

<!--#region Conceitos de Segurança -->

<details><summary>Conceitos de Segurança</summary>

<ul>
    <li><a href="#conceitos-apresentacao">Apresentação do Módulo</a></li>
    <li><a href="#conceitos-autenticacao">O que é autenticação</a></li>
    <li><a href="#conceitos-autorizacao">O que é autorização</a></li>
    <li><a href="#conceitos-api">Autenticação e Autorização em APIs</a></li>
    <li><a href="#conceitos-token">Onde armazenar o Token</a></li>
    <li><a href="#conceitos-jwt">O que é JWT</a></li>
    <li><a href="#conceitos-payload">Entendendo Payload e assinatura do Token</a></li>
    <li><a href="#conceitos-refresh">Refresh Token</a></li>
</ul>

</details>

<!--#endregion -->

<!--#region JWT e Bearer na Prática -->

<details><summary>JWT e Bearer na Prática</summary>

<ul>
    <li><a href="#jwt-bearer-projeto">Criando o projeto</a></li>
    <li><a href="#jwt-bearer-configuracao">Criando o arquivo de configuração</a></li>
    <li><a href="#jwt-bearer-usuario">Criando a classe de usuário</a></li>
    <li><a href="#jwt-bearer-tokenservice">Iniciando o Token Service</a></li>
    <li><a href="#jwt-bearer-assinatura">Assinando o Token</a></li>
    <li><a href="#jwt-bearer-geracao">Gerando o Token</a></li>
    <li><a href="#jwt-bearer-claims">Entendendo os Claims</a></li>
    <li><a href="#jwt-bearer-claimsidentity">Claims Identity</a></li>
    <li><a href="#jwt-bearer-payload">Payload</a></li>
    <li><a href="#jwt-bearer-adic-autenticacao">Adicionando autenticação</a></li>
    <li><a href="#jwt-bearer-config-autenticacao">Configurando a autenticação</a></li>
    <li><a href="#jwt-bearer-testes">Testando o Token</a></li>
    <li><a href="#jwt-bearer-policies">Policies</a></li>
    <li><a href="#jwt-bearer-hack">Hackeando o Token</a></li>
    <li><a href="#jwt-bearer-get-claims">Obtendo Claims do Token</a></li>
    <li><a href="#jwt-bearer-claimsidentityextension">Claims Identity Extension</a></li>
</ul>

</details>

<!--#endregion -->

<!--#region Criando um Sistema de Login -->

<details><summary>Criando um Sistema de Login</summary>

<ul>
    <li><a href="#login-projeto">Criando o projeto</a></li>
    <li><a href="#login-entity-vo">Entity e Value Object</a></li>
    <li><a href="#login-string-extension">String Extension</a></li>
    <li><a href="#login-vo-email">Value Object de Email</a></li>
    <li><a href="#login-vo-email-verif">Value Object de Verificação de E-mail</a></li>
    <li><a href="#login-vo-senha">Value Object de Senha</a></li>
    <li><a href="#login-senha-aleat">Gerando senhas aleatórias</a></li>
    <li><a href="#login-password">Password Hashing</a></li>
    <li><a href="#login-hashes">Comparando Hashes</a></li>
    <li><a href="#login-user-entity">Finalizando a entidade User</a></li>
    <li><a href="#login-user-map">Mapeando o User</a></li>
    <li><a href="#login-datacontext">Criando o DataContext</a></li>
    <li><a href="#login-api-config">Configurando a API</a></li>
    <li><a href="#login-api-organizer">Organizando a API</a></li>
    <li><a href="#login-bdados">Gerando o banco de dados</a></li>
    <li><a href="#login-usecases">Use Cases</a></li>
    <li><a href="#login-response">Response</a></li>
    <li><a href="#login-response-comp">Compondo a resposta</a></li>
    <li><a href="#login-specification">Specification</a></li>
    <li><a href="#login-repo-services">Repositórios e Serviços</a></li>
    <li><a href="#login-req-valid">Validando a requisição</a></li>
    <li><a href="#login-entity-vo-ger">Gerando entidades de value objects</a></li>
    <li><a href="#login-repo-interag">Interagindo com repositórios</a></li>
    <li><a href="#login-handler">Finalizando o handler</a></li>
    <li><a href="#login-repo-impl">Implementando o repositório</a></li>
    <li><a href="#login-service-impl">Implementando o serviço</a></li>
    <li><a href="#login-api-config-2">Configurando a API</a></li>
    <li><a href="#login-depend-reg">Registrando as dependências</a></li>
    <li><a href="#login-mediator">Adicionando suporte ao Mediator</a></li>
    <li><a href="#login-met-post">Criando o método POST</a></li>
    <li><a href="#login-user-secrets">dotnet user secrets</a></li>
    <li><a href="#login-api-test">Testando a API</a></li>
    <li><a href="#login-auth-usecase">Authenticate Use Case</a></li>
    <li><a href="#login-auth-handlers">Authenticate Handler</a></li>
    <li><a href="#login-auth-repo">Authenticate Repository</a></li>
    <li><a href="#login-jwt-service">JWT Service</a></li>
    <li><a href="#login-token-ret">Retornando o Token</a></li>
    <li><a href="#login-roles">Criando Roles</a></li>
    <li><a href="#login-roles-map">Mapeando Roles</a></li>
    <li><a href="#login-roles-get">Recuperando os Roles</a></li>
    <li><a href="#login-roles-add">Adicionando Roles ao Token</a></li>    
    <li><a href="#login-roles-util">Utilizando os Roles</a></li>
    <li><a href="#login-conclusao">Conclusao</a></li>
</ul>

</details>

<!--#endregion -->

<!--#endregion -->

<!--#region Conceitos de Segurança -->

<h2 id="conceitos">Conceitos de Segurança</h2>

<!--#region Apresentação do Módulo  -->

<details id="conceitos-apresentacao"><summary>Apresentação do Módulo</summary>

<br/>

- Neste módulo vamos entender os principais conceitos de **autenticação** e **autorização**
- O que são Tokens, como eles são gerados, como inspecionamos eles e quais padrões, como **JWT** temos disponíveis
- Ao término deste módulo, você terá uma **visão completa** de como funciona a autenticação em APIs e Poderá utilizar estes conceitos para implementar estes **princípios em qualquer tecnologia**

</details>

<!--#endregion -->

<!--#region O que é autenticação  -->

<details id="conceitos-autenticacao"><summary>O que é autenticação?</summary>

<br/>

<h3>O que é autenticação?</h3>

Autenticação é o processo que **diz quem você é**

Por exemplo, em um processo de autenticação interno ou externo, eu estou garantindo que sou o **André Baltieri**, através do e-mail **xyz@balta.io**

Este processo pode ser feito de **diferentes maneiras**:

- usuário e senha
- e-mail e senha
- redes sociais, os famosos **Login com Google**, **Login com Facebook**

De qualquer forma, não importa como. 

<h3>O processo sempre é o mesmo!</h3>

Garantir que quem está dizendo que é **xyz@balta.io** é realmente o **André Baltieri**

<h3>Mas como garantir que eu sou eu?</h3>

O primeiro passo que precisamos é garantir que uma pessoa está ligada a um **e-mail**, **telefone** ou um **nome de usuário**. 

Este processo é relativamente simples.

No caso da garantia de um e-mail ser de quem ele realmente disse que é, basta no processo de registro do usuário, enviar um e-mail com um código a ele.

![Conceitos](./Assets/Images/conceitos-01.png)

<h3>Autenticação Externa</h3>

Neste modelo, você **delega a responsabilidade** para outro servidor, o que pode ser uma boa já que o processo de verificação do **Google** ou **Microsoft** por exemplo são bem mais **complexos** e **completos** do que possivelmente o seu será.

Neste formato, o que fazemos é no login, gerar um token de ativação e redirecionar o usuário para uma plataforma externa.

Após autenticado, o usuário retorna para nossa plataforma com um token e assim damos andamento na requisição.

Em suma, qualquer pessoa pode fornecer este serviço, basta realizar a implementação do **OIDC (Open Id Connect)**, um protocolo aberto de autenticação.

Existem servidores **OIDC** prontos como o **Identity Server** ou **Keycloak**, ambos fornecem uma ótima implementação e são completos em recursos.

Resumindo, se o **balta.io** tivesse uma implementação **OIDC**, você poderia adicionar um botão **Login com balta** em seu site.

Como o custo e risco de manter um **OIDC** próprio são altos, a recomendação é sempre começar do mais básico, implementando autenticação **oAuth** com **JWT**.

</details>

<!--#endregion -->

<!--#region O que é autorização  -->

<details id="conceitos-autorizacao"><summary>O que é autorização?</summary>

<br/>

<h3>O que é autorização?</h3>

Se *autenticação diz quem você é*, **autorização diz o que você pode fazer**

São os famosos **Roles** ou Perfis, e que no ASP.NET se estendem para políticas (**Policies**) e afirmações (**Claims**).

Enquanto a autenticação segue em diversas vezes uma **padronização**

**A autorização não tem necessariamente uma regra**

Eu mesmo já fiz sistemas onde ao invés de **Roles** utilizávamos **Tags**

<h3>De qualquer forma, a ideia aqui é, sabendo que "xyz@balta.io" é o "Andre Baltieri" o que ele pode fazer dentro deste sistema?</h3>

Note que estamos falando **DESTE** sistema, pois a autorização varia muito, inclusive entre módulos, páginas e até mesmo botões.

Podem haver páginas no sistema que eu posso ver, mas não posso editar e a autorização precisa tratar tudo isto.

Na maioria dos casos também, a autorização é **CUMULATIVA** ou seja, eu posso ter vários perfis como "admin", "employee", "sales" e cada um deles ter funções distintas que são acumuladas.

</details>

<!--#endregion -->

<!--#region Autenticação e Autorização em APIs  -->

<details id="conceitos-api"><summary>Autenticação e Autorização em APIs</summary>

<br/>

Como você já deve imaginar, tudo começa na API, visto que a segurança no lado do cliente é sempre fraca, todo processo deve rodar no servidor.

Armazenar um usuário e seus perfis é uma tarefa relativamente simples, incluindo ler estes dados e enviar para a tela, o problema está no armazenamento destes dados do outro lado.

Deixa eu te explicar melhor, em APIs **nós nunca ficamos autenticados ou autorizados**, a cada requisição este processo é feito.

<h3>Isto se repete para toda requisição</h3>

Existe um motivo para isto, e até um tempo atrás, utilizávamos **sessão** para manter estes dados em memória e o usuário permanecer conectado.

Com a distribuição das aplicações em diferentes servidores, manter o usuário conectado *não é algo viável*, pois os **servidores não compartilham memória**

Então imagina que você acessou o site do **balta.io** agora e fez o login, o servidor armazenou seus dados de login em **memória** e você está agora visualizando uma aula com **10 minutos de duração**.

Após terminar de ver a aula, você clica no botão concluir, porém, o servidor que você se autenticou previamente está **ocupado**.

**Neste momento entra em ação o Load Balancer ou balanceador de carga**

Ele rapidamente identifica que existe outro servidor do **balta.io** e que está **livre**, desocupado, então manda sua requisição para lá.

Como os servidores **não compartilham memória**, logo, você não está autenticado neste servidor e sua requisição falha com o erro **401 - Unauthorized**

Mudando o cenário para autenticação que temos hoje, onde a **cada requisição você precisa se autenticar**, este erro deixa de acontecer.

Neste modelo, geramos um Token de acesso, baseado em uma **chave privada que só o servidor tem** (ela tem que ser comum entre os servidores) e então a cada requisição, o **Frontend** envia este token.

Com o **Token** em mãos, como temos a chave privada, conseguimos **desencriptar** ele e obter os valores do usuário (e quaisquer outros valores que ele tenha)

Você pode também armazenar os **Tokens** para uma maior validação, mas isto implica em pelo menos **uma requisição no banco de dados** a cada requisição autenticada que sua API recebe.

</details>

<!--#endregion -->

<!--#region Onde armazenar o Token  -->

<details id="conceitos-token"><summary>Onde armazenar o Token</summary>

<br/>

<h3>Onde armazenar o Token?</h3>

**Não faz sentido!!!**

Foi a **primeira coisa que pensei** quando vi que os tokens devem ser armazenados pelo **Frontend** e enviados a cada requisição.

**mas deixa eu te explicar melhor este processo**

Se precisamos enviar o **token** a cada requisição, já que não ficamos autenticados nas APIs, precisamos armazená-los em algum lugar.

<h3>Mas onde???</h3>

<h3>Session Storage</h3>

```js
sessionStorage.setItem('chave','valor'); // salva um valor
sessionStorage.getItem('chave'); // lê um valor
sessionStorage.removeItem('chave'); // remove um item
sessionStorage.clear(); // limpa todos os dados
```

<h3>Local Storage</h3>

```js
localStorage.setItem('chave','valor'); // salva um valor
localStorage.getItem('chave'); // lê um valor
localStorage.removeItem('chave'); // remove um item
localStorage.clear(); // limpa todos os dados
```

<h3>Cookies</h3>

Os **Cookies** são **automaticamente anexados** nos cabeçalhos das requisições e alguns modelos de autenticação como as do ASP.NET MVC e ASP.NET Razor Pages **trabalham com Cookies**

É importante lembrar que ambos os modelos citados acima **são diferentes** do que estamos implementando aqui, por isto **Cookies** fazem sentido para o cenário.

---

De qualquer forma, tratar quando e como queremos compartilhar o **Token** de autenticação pelo **Local Storage** é a *melhor opção* para nosso cenário. 

<h3>Domínios e Sub Domínios</h3>

É importante lembrar também que o armazenamento local (**Session e Local**) são baseados nos **domínios** e/ou **sub domínios**, o que significa que informações persistidas nas sessões do site **balta.io** por exemplo, não serão visíveis nas sessões dentro do site **microsoft.com**

Desta forma, **não temos como compartilhar informações** entre **storages** de diferentes **domínios** ou **sub domínios**.

No caso dos **Cookies**, existem políticas que permitem estas trocas de informações, recursos como **Same Site** e troca de origem, desde que atribuídos de forma correta e consciente tornam o **Cookie** uma ótima opção para **Single Sign On** por exemplo. 

<h3>Banco de Dados</h3>

Sim, existe um banco de dados que roda dentro do *browser* chamado **IndexDb** e usando o **Blazor WASM** (WASM - Web Assembly) conseguimos até rodar o **SQLite** no *browser*.

De qualquer forma, as restrições são as mesmas do **Local Storage**, eles duram até serem removidos mas com a vantagem de armazenar mais informações (tamanho em disco)

Como no nosso caso, precisamos apenas de uma chave e valor e o **Local Storage** nos oferece até **200MB** (isto varia de acordo com o *browser*), podemos novamente ficar com o **Local Storage que é mais fácil, leve e simples**

</details>

<!--#endregion -->

<!--#region O que é JWT  -->

<details id="conceitos-jwt"><summary>O que é JWT</summary>

<br/>

<h3>O que é JWT?</h3>

Então você está me dizendo que eu vou armazenar um **Token** em um local onde o usuário ou outra pessoa **pode ir lá e visualizar**?

Isto mesmo, os **Tokens** são como **chaves de acesso** com informações e uma duração, ou seja, se alguém obter seu **Token**, ele pode **impersonar** ou fingir que é você.

Por isto a **segurança física** é a primeira e mais importante etapa que temos. 

Se alguém tem acesso ao seu **browser** fisicamente (ou remotamente) ele pode ver seu **Token**

Na verdade, este seria o **menor dos seus problemas**, já que os dados de navegação são armazenados localmente, ou seja, todas as suas sessões estão em um arquivo. 

Basta copiar este arquivo da sua máquina para a minha e pronto, **estarei logado com todas as suas sessões**

<h3>Por este motivo frequentemente somos recomendados a não clicar em links suspeitos, visto que uma simples cópia expõe todas as suas informações</h3>

Mas voltando aos **Tokens**, embora você possa armazenar uma chave/valor no **Local Storage**, é legal **encriptar** estas informações, correto?

Desta forma, se alguém roubar seu **Token** não verá nada além de uma **Hash* como esta abaixo:

```ps
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

Como este processo é algo **comum entre as aplicações e APIs**, criou-se um padrão chamado **JWT** (pronuncia-se JÓT), que é a sigla para **Json Web Token**

Ao desencriptar este **Token**, temos como resultado os seguintes JSON:

```json
{
    "alg": "HS256",
    "typ": "JWT"
},
{
    "sub": "1234567890",
    "name": "John Doe",
    "iat": 1516239022
}
```

Se você notar, o **Token** contém "." para segmentar suas regiões e o mesmo é dividido em três partes principais.

---

A **primeira parte** é chamada de **Header** ou cabeçalho, que define o **algoritmo utilizado** na encriptação e o **tipo do Token**, no nosso caso. JWT.

Quando **desencriptamos** ela, temos o seguinte JSON como resultado:

```json
{
    "alg": "HS256",
    "typ": "JWT"
}
```

Podemos **mudar estes valores**, incluindo o algoritmo e assim teremos valores diferentes. No caso, mudando de **HS256** para **HS384**, temos a seguinte **Hash** e **Header**:

```json
//eyJhbGciOiJIUzM4NCIsInR5cCI6IkpXVCJ9
{
    "alg": "HS384",
    "typ": "JWT"
}
```

<h3>O mais comum e recomendado até o momento da escrita deste artigo é o HS256, ele balanceia performance e segurança. Quanto mais alta a encriptação mais processamento ela requer</h3>

--- 

O **segundo item** é o **Payload** ou carga, que são informações que podemos incluir no **Token** com algumas ressalvas.

```json
{
    "sub": "1234567890",
    "name": "John Doe",
    "admin": true,
    "iat": 1516239022
}
```

Assim como nos **Headers** o **Payload** também varia de acordo com a quantidade de informações que colocamos nele. Ao incluirmos a informação **premium** temos um outro valor sendo gerado. 

```json
{
    "sub": "1234567890",
    "name": "John Doe",
    "admin": true,
    "premium": true,
    "iat": 1516239022
}
```

---

Por fim temos o **terceiro item** que representa a **assinatura** do **Token**, que só existe no lado do servidor.

Em resumo o que temos é uma encriptação **SHA256** de três itens convertidos para **Base64**, um código simples assim:

```js
HMACSHA256(
    base64UrlEncode(header) + "." +
    base64UrlEncode(payload),
    'MINHA CHAVE SECRETA'
)
```

---

<h3>Então quer dizer que, além de salvar o Token eu ainda posso visualizar ele? Sim, tem sites com o jwt.io que te permite visualizar tudo que um Token contém</h3>

Indo além disso, o **jwt.io** (você pode fazer isto manualmente também) te permite alterar o código de um **Token**, adicionando informações extras.

Isto significa que se eu pegar o **Token** abaixo, gerado para mim que diz que eu só tenho o perfil **student**:

```json
{
    "name":"André Baltieri",
    "roles": ["student"],
    "iat": 1516239022
}
```

<h3>E adicionar o perfil "admin":</h3>

```json
{
    "name":"André Baltieri",
    "roles": ["student", "admin"],
    "iat": 1516239022
}
```

Agora eu tenho um novo **Token**, com acesso de administrador:

```jwt
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiQW5kcsOpIEJhbHRpZXJpIiwicm9sZXMiOlsic3R1ZGVudCIsImFkbWluIl0sImlhdCI6MTUxNjIzOTAyMn0.gs12hMRKMhjboHF4Arw7R9r7MQUQTsKJNs6YxdDEC4w
```

</details>

<!--#endregion -->

<!--#region Entendendo Payload e assinatura do Token  -->

<details id="conceitos-payload"><summary>Entendendo Payload e assinatura do Token</summary>

<br/>

<h3>Payloads e Assinaturas</h3>

Muita calma nesta hora! **É possível sim mudar um Token**, mas para isto é necessário uma chave privada que somente o servidor deve conter.

<h3>Se o Token for gerado com qualquer outra chave, diferente da qual foi gerada, o mesmo é invalidado. Isto é o que torna os Tokens seguros em relações as mudanças</h3>

Então não importa se o **Token** foi alterado no cliente ou mesmo no **meio do caminho**, se ele não for regerado com a **chave privada** (que só o servidor deve conhecer) ele será invalidado. 

**MAN IN THE MIDDLE** existe um ataque comum chamado "Homem no meio" que basicamente intercepta a comunicação entre o cliente e o servidor e rouba informações ou modifica elas.

<h3>Não armazene valores sensíveis no Payload</h3>

Outro ponto importantíssimo é sobre o **uso do Payload**. Embora você possa adicionar qualquer informação que desejar nele, **não é recomendado trafegar informações sensíveis ali**

Cartão de crédito, telefone, endereço ou qualquer informação que comprometa os dados do seu usuário, **devem ser mantidos apenas no servidor**

Em suma, tendo o e-mail ou ID do usuário no **payload** já basta. Com estas informações você pode **consultar o que quiser sobre ele**

<h3>Tempo de Vida do JWT</h3>

Uma recomendação e padrão dos **JWT**s é conter no **Payload** a informação **iat** que significa **Issued At** ou **Gerado em**, que nada mais é do que um **timestamp** da data/hora que o **Token** foi gerado. 

Desta forma, podemos criar uma validação para o **Token**, dizendo que o mesmo **só pode existir por X tempo**. Assim, se um **Token** for roubado, ele só vai ser **útil durante XX dias ou horas**, passado isto ele se torna inválido.

```json
{
    "sub": "1234567890",
    "name": "John Doe",
    "iat": 1516239022
}
```

<h3>Dependendo do sistema este tempo de expiração pode variar, mas em geral, ele não deve ser tão curto a ponto de incomodar o usuário, que precisará se autenticar o tempo todo, nem tão longo que alguém possa roubar e usar por meses.</h3>

</details>

<!--#endregion -->

<!--#region Refresh Token  -->

<details id="conceitos-refresh"><summary>Refresh Token</summary>

<br/>

Caso opte pelo uso de um tempo reduzido no tempo de vida dos Tokens, uma ótima alternativa é o uso dos **Refresh Tokens**

Sempre que gerar um **Token** para o seu usuário, gere uma **chave aleatória junto**, encriptada e dado o **Token** anterior e mais esta nova chave, **um novo Token pode ser gerado**

No caso, é interessante gerar este novo **Token** em um **intervalo menor que a expiração do Token principal**, desta forma, o usuário sempre mantém a sessão ativa.

Em adicional, você pode optar por **não gerar um novo Token** caso o **anterior já tenha expirado**, isto varia bastante incluindo a base em relação tempo de vida do **Token**.

Em resumo, supondo que meu **Token** expira em 2 horas, como eu sei o quanto ele dura e quando foi emitido, **posso me prontificar e já gerar um novo Token** (refresh) desde que a chave do **Refresh Token** tenha sido enviada junto.

Caso meu **Token já tenha expirado**, mas há menos de 2 horas podemos manter o processo acima e também gerar o **Token** (Refresh)

Caso meu **Token** tenha **expirado há mais de 2 horas**, aí não tem jeito, o usuário precisa se autenticar novamente. 

</details>

<!--#endregion -->

<!--#endregion -->

<!--#region JWT e Bearer na Prática -->

<h2 id="jwt">JWT e Bearer na Prática</h2>

<!--#region  -->

<details id=""><summary></summary>

<br/>



</details>

<!--#endregion -->

<!--#endregion -->

<!--#region Criando um Sistema de Login -->

<h2 id="login">Criando um Sistema de Login</h2>

<!--#region  -->

<details id=""><summary></summary>

<br/>



</details>

<!--#endregion -->

<!--#endregion -->