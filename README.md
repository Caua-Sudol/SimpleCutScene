# SimpleCutScene

Protótipo em MonoGame para entender como implementar um sistema de cutscene: uma máquina de estados simples que interrompe o controle do jogador, roda uma sequência de câmera e movimento, e depois devolve o controle.

Projeto ainda em andamento, feito para estudar o problema antes de aplicá-lo em algo maior.

## Sobre o projeto

O jogador controla um personagem numa cena simples com uma porta e um chão. Ao encostar na porta, a cena troca do modo `PLAYING` para `CUTSCENE`: a tela escurece com um fade, o jogador é reposicionado, a câmera aproxima com zoom, e o personagem anda sozinho até um ponto fixo antes da cena voltar ao normal.

Estados da cena (`GameMode`):

- `PLAYING`: controle normal do jogador.
- `CUTSCENE`: sequência automática, sem input do jogador.

## Estrutura

- `Game1.cs`: ponto de entrada, câmera e loop principal.
- `Scene.cs`: máquina de estados da cutscene, controle de fade e transição entre `PLAYING` e `CUTSCENE`.
- `Player.cs`: posição, movimento e colisão do jogador.
- `Door.cs` / `Floor.cs`: elementos estáticos da cena que definem o gatilho da cutscene.
- `Camera.cs`: posição, zoom e matriz de transformação da câmera.

## Status atual

Em andamento. O ponto que está sendo testado agora é o ciclo de fade entre os modos: em determinadas condições ele trava numa etapa intermediária e não retorna corretamente para `PLAYING`. É o próximo problema a resolver antes de tentar sequências mais longas de câmera e movimento.

## Como executar

```bash
dotnet restore
dotnet run
```

Controles: `WASD` para mover o jogador, `Esc` para sair. Encoste na porta para acionar a cutscene.
