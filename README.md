# Sobre

Protótipo criado para Infinity Games + Holoboo. Baseado no game: https://play.google.com/store/apps/details?id=com.infinitygames.loopenergy.

O desenvolvimento envolveu entender como o jogo original funcionava. Realizei a análise do funcionamento, pontuações e levels, além da identidade visual. Como o desafio especificava a mecânica de arraste para conexão dos pontos, achei que a melhor maneira de implementar seria através da criação de um grid, onde cada bloco representaria um node com suas características próprias. A criação desse grid é realizada seguindo algumas regras determinadas pelo desenvolvedor e outras pré-determinadas em código. O desenvolvedor pode escolher a largura e altura do grid além de quantas baterias, conectores e espaços em branco, mas, uma bateria sempre estará a uma unidade de distancia de um conector.

Uma vez realizada a criação do mapa, o objetivo do jogo é conectar as baterias aos conectores, evitando espaços em branco no caminho (no momento não há condição de derrota, o espaço em branco serve apenas como obstáculo), a condição de vitória é traçar uma, ou mais, linhas de uma bateria (toda conexão sempre começa em uma bateria) até todos os conectores da fase. Cada conector tem sua propria regra de conexão e seu feedback (som ou visual) uma vez conectados.

Pontos positivos:
- Criação do mapa: A criação é feita rapidamente seguindo regras estabelecidas, garantindo que nenhum jogo será igual ao outro;
- Mecânicas;
- Gamefeel;
- Visual;

O que não deu certo:
- Gravação de leveis em Scriptable Object;
- Sentimento de objetivo: Não há desafio em conectar os pontos, como possível solução, implementar mecânica que impossibilita duas conexões de ocuparem o mesmo node, representar isso visualmente.
- Feedback de progresso: Não há um feedback imediato sobre a performance do jogador, como possível solução, implementar um sistema de pontos onde o caminho realizado pelo jogador é comparado ao caminho ótimo até o conector.

Bugs conhecidos:
- Grids com largura e largura igual a 1 impedem o jogo de começar devido às regras de distanciamento de nodes.
- Mapas com largura superior à 6 nodes impactam o layout em alguns aparelhos mobile. Sugestão: Alterar o tamanho dos tiles conforme a largura do mapa e a tela do celular.

## Etapas do desafio:

- Tem que ser jogável em modo portrait;
- O source tem que ser capaz de gerar níveis procedimentalmente, e tem que ser capaz de gravar níveis em Scriptable Object;
- Gravar o progresso e a pontuação do jogador localmente (usar Playerprefs);
- Ter um menu com os níveis. O jogador não pode jogar níveis a seguir ao nível que tem desbloqueado, tendo que jogar o nível actual para desbloquear os seguintes;
- Boa legibilidade;
- Bom juice (reacção imediata a input, efeitos de partículas, camera shake, etc);
- Ao vencer o nível as linhas têm que iluminar-se ou fazer algo do género para que o jogador se sinta feliz por ter terminado;
- O código deve ser o mais sucinto possível e o mais legível possível, dando-se prioridade à legibilidade. Deve estar suficientemente documentado internamente; não em abundância;
- A arquitectura de pastas tem que fazer sentido para seres humanos em geral;
- A UI do jogo tem que ser feita em Unity Canvas, e tem que manter-se estável (não distorcer ou desformatar) para vários formatos de ecrã portrait;
- À medida que vais desenvolvendo o projecto tens que fazer commits para um repositório, os commits têm que ser compreensíveis, lógicos, e têm que seguir um fio condutor; os mesmos critérios se aplicam à timeline do repositório;
- O jogo tem que estar preparado para funcionar em Android e iOS, optimizado para tal;

## Downloading
    Android APK: https://drive.google.com/file/d/1zV9Ii-ZIM7XdC5mH19XGKO6C8vcgJQCk/view?usp=sharing

## Contribuindo
The software was developed using C# and Unity 2020.1.9f1.
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
Please make sure to update tests as appropriate.

## Créditos
Icons made by https://www.flaticon.com/authors/pixel-perfect from https://www.flaticon.com/

Icons made by https://www.flaticon.com/authors/fjstudio from https://www.flaticon.com/

Icons made by https://www.flaticon.com/authors/roundicons from https://www.flaticon.com/

Music by https://opengameart.org/content/soliloquy

Sound effects by https://opengameart.org/content/sound-effects-pack

Sound effects by https://freesound.org/people/Eponn/sounds/531511/

## Licensa
[MIT](https://choosealicense.com/licenses/mit/)