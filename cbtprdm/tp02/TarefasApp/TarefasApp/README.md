# TarefasApp (.NET MAUI)

Aplicativo de lista de tarefas com navegação hierárquica e modais, construído em .NET MAUI.
Nome: Bianca Fonseca Dantas Ribeiro - CB3025683

## Estrutura do projeto

```
TarefasApp/
├── TarefasApp.csproj
├── MauiProgram.cs          # Configuração de DI (injeção de dependência)
├── App.xaml / App.xaml.cs  # Recursos globais (cores, estilos)
├── AppShell.xaml(.cs)      # Shell: define a navegação e registra as rotas
├── Models/
│   └── TarefaItem.cs       # Modelo da tarefa (Titulo, Descricao, DataCriacao, Prioridade)
├── Services/
│   └── TarefaService.cs    # Serviço singleton com a lista de tarefas (ObservableCollection)
├── Converters/
│   └── PrioridadeParaCorConverter.cs
└── Views/
    ├── MainPage.xaml(.cs)            # Lista de tarefas (CollectionView)
    ├── TarefaDetailPage.xaml(.cs)    # Detalhes da tarefa (navegação hierárquica)
    ├── EditarTarefaPage.xaml(.cs)    # Modal de edição
    └── AdicionarTarefaPage.xaml(.cs) # Modal de adição
```

## Como as exigências do exercício foram implementadas

1. **Lista de tarefas (página inicial)** — `MainPage.xaml` usa um `CollectionView` (substituto
   moderno do `ListView`) com `DataTemplate` mostrando título, descrição e uma faixa colorida
   de prioridade.

2. **Navegação hierárquica** — `AppShell` registra a rota `TarefaDetailPage`. Ao tocar em um
   item (`TapGestureRecognizer`), `MainPage` chama `Shell.Current.GoToAsync(nameof(TarefaDetailPage), parametros)`,
   empilhando a página de detalhes sobre a pilha de navegação (com seta de "voltar" automática).

3. **Página de detalhes** — mostra título, descrição, data de criação e prioridade, recebidos
   via `IQueryAttributable` (passagem de dados entre páginas pelo Shell).

4. **Botão "Editar" (modal)** — abre `EditarTarefaPage` com `Navigation.PushModalAsync`,
   passando a tarefa atual pelo construtor para pré-preencher os campos. Ao salvar, o
   `TarefaService` atualiza a tarefa (que implementa `INotifyPropertyChanged`, então a lista
   e os detalhes refletem a mudança automaticamente).

5. **Botão "Excluir"** — usa `DisplayAlert` como diálogo de confirmação antes de remover a
   tarefa do `TarefaService` e voltar para a lista.

6. **Botão "Adicionar" (modal)** — na página inicial, abre `AdicionarTarefaPage` como modal
   (`PushModalAsync`), permitindo informar título, descrição, data de criação e prioridade de
   uma nova tarefa.

7. **Passagem de dados entre páginas** — feita de duas formas:
   - Shell `GoToAsync` com dicionário de parâmetros + `IQueryAttributable` (lista → detalhes);
   - Injeção via construtor da página modal (detalhes → edição), já que modais são criados
     diretamente com `new`.

8. **Layout** — cores e estilos centralizados em `App.xaml` (cards com `Frame` e sombra,
   cores de prioridade, botões arredondados) para uma aparência limpa e consistente.