# Exploding Elves
**EN-US 🇺🇸**
This project was  made for a selective process and the start idea sent by Fortis Games.

In **Exploding Elves** the player can control the interval in seconds for each of the 4 elves house (Blue, Black, Red and White) to spawn a single elf. If the interval is 0s the spawn is turned off and no elf will be spawned. Every time elves of the same type/color met they spawn a new one if possible (always on the position of the elder elf + (0, 1, 0)) and every time elves of different type/color met they explode and kill each other. Since a performance threshold is detected the game avoid to spawn new elves until the game can runs stable again.

This project was made with a week deadline. Made in Unity (2022.3.13) with C# code.

**PT-BR 🇧🇷**
Este projeto foi feito para um processo seletivo e a ideia inicial foi enviada pela Fortis Games.

Em **Exploding Elves**, o jogador pode controlar o intervalo em segundos para cada uma das 4 casas de elfos (Azul, Preto, Vermelho e Branco) gerar um único elfo. Se o intervalo for 0s, a geração é desativada e nenhum elfo será gerado. Cada vez que elfos do mesmo tipo/cor se encontram, eles geram um novo, se possível (sempre na posição do elfo mais velho + (0, 1, 0)), e cada vez que elfos de tipos/cores diferentes se encontram, eles explodem e se eliminam mutuamente. Uma vez que um gargalo de desempenho é detectado, o jogo evita gerar novos elfos até que ele possa funcionar de forma estável novamente.

Este projeto foi executado com o prazo de uma semana. Feito na Unity (2022.3.13) com código C#.

## Project Organization

```bash
├── Asssets
│   ├── Animation
│   │   ├── **/*.controller
│   ├── Materials
│   │   ├── **/*.mat
│   ├── Models
│   │   ├── **/*.fbx
│   ├── Plugins
│   │   ├── Demigant (DoTween)
│   ├── Prefabs
│   │   ├── Characters
│   │   ├── Spawners
│   │   ├── UI
│   │   ├── Utils
│   ├── Resources
│   ├── Scenes
│   │   ├── GameScene.unity
│   ├── Scripts
│   │   ├── CharacterSystem
│   │   ├── Extensions
│   │   ├── SpawnSystem
│   │   ├── UI
│   ├── Settings
│   ├── TextMesh Pro
```

### Prefabs
**EN-US 🇺🇸**
Inside Prefabs folder we have four groups: 

**Characters :** containing the base BlueCharacter and three prefab variants for Black, Red and White
**Spawners :** containing the base BlueSpawner and three prefab variants for Black, Red and White
**UI :** containing two UI panels used for elves population info and memory log
**Utils :** containing the Battlefield prefabs and Camera.

**PT-BR 🇧🇷**
Dentro da pasta Prefabs, temos quatro grupos:

**Characters:** contendo o *BlueCharacter* base e três variantes de prefab para *Black*, *Red* e *White* 
**Spawners:** contendo o *BlueSpawner* base e três variantes de prefab para *Black*, *Red* e *White* 
**UI:** contendo dois painéis de UI usados para informações de população de elfos e registro de memória 
**Utils:** contendo os prefabs de *Battlefield* e Camera.

### Scripts
**EN-US 🇺🇸**
Inside Scripts folder we also have four namespaces:
 
 **CharacterSystem :** Our main game system, this one creates, move, and animate the elves in game. To make this possible we have four files: 
-   _CharacterAnimator.cs_: A simple MonoBehaviour with the animator as a serialized field, Contains all animator parameters as constants and fire them according to the *CharacterInter actionController*
-   _CharacterInteractionController.cs_: The main controller for characters behavior checking collisions and then telling if the character needs to attack and die or spawn a friend. also contains the *CharacterAnimator* and *CharacterMovementController* instances to tell them how to deal with the necessities. Here we have Id and Character type too, used for logic comparisons and defining if the character should spawn a friend or kill an enemy.
-   _CharacterMovementController.cs_: Responsible for all the character random movement across the battlefield, here every random interval between 1 and 5 seconds the character change to a new direction and keeps walking using the Rigidbody's physics.
-    _CharacterWarehouse.cs_: In this file we actually have four classes, The CharacterType Enum that will define the elf color. ModelInfo and ModelsDataManager used to serialize a Dictionary for *CharacterWarehouse*, this one that will be a singleton capable of create and instantiate each of the characters according to the CharacterType. This Class will also control the population statistics and how many elves of each kind were spawned, here we will find our main performance controller, since the **CreateCharacter** function will only do something if the Frame Rate is higher than 20. Making possible to every device to have a great amount of elves existing at the same time the animations run with no problems.
 
**Extensions :**  This namespace isn't being used like an place for the extension classes as it meant to be. but more like an utils or commons. here we have three files:
-   _CameraExtensions.cs_: A real Extension static class home of methods to calculate a screen aspect ratio based on our game camera or proportional Vectors and floats.
-   _AspectRatioResizer.cs_: A MonoBehaviour that receives two Vector3 min and max scale and using CameraExtensions define a new proportional scale for the transform attached.
-   _PerformanceManager.cs_: A simple static class used to provide FPS and memory usage information for the entire game

**SpawnSystem :** Here is everything we need to our elves houses work. Also the main player inputs happen here:
-   _SpawnerController.cs_: Attached to every Spawner prefab, *SpawnerController* receives the plus and minus Button, a spawn point Transform, the TimerView and the CharacterType that should be spawned, all as SerializedFields. Then use these guys to manage and apply the logic needed to spawn a new elf and increase or decrease this interval. The increase and decrease step is defined by the SPAWN_INTERVAL_DELTA const defined as 1 second, also if the interval is zero, no elf will be spawned.
-   _TimerView.cs_: This script is used and called by *SpawnerController* to show players the interval in seconds that will be used between each elf spawn, at the TMP on the top of each spawner.

**UI :** The two view scripts for both UI prefabs, *PerformanceUI.cs* script that will update MemoryLog and *PopulationUI.cs* script that will update Population prefab

**PT-BR 🇧🇷**
Dentro da pasta *Scripts*, também temos quatro namespaces:

**CharacterSystem:** Nosso sistema principal do jogo, este cria, move e anima os elfos no jogo. Para tornar isso possível, temos quatro arquivos:
-   _CharacterAnimator.cs_: Um MonoBehaviour simples com o animador como um campo serializado, contém todos os parâmetros do animador como constantes e os dispara de acordo com as demandas do *CharacterInteractionController*
-   _CharacterInteractionController.cs_: O controlador principal do comportamento dos personagens verifica as colisões e depois informa se o personagem precisa atacar e morrer ou gerar um amigo. também contém as instâncias *CharacterAnimator* e *CharacterMovementController* para dizer-lhes como lidar com as necessidades. Aqui também temos Id e Tipo de personagem, usados ​​para comparações lógicas e definir se o personagem deve gerar um amigo ou matar um inimigo.
-   _CharacterMovementController.cs_: Responsável por todo o movimento aleatório do personagem no campo de batalha, aqui a cada intervalo aleatório entre 1 e 5 segundos o personagem muda para uma nova direção e continua andando usando a física do Rigidbody.
-    _CharacterWarehouse.cs_: Neste arquivo, na verdade, temos quatro classes, O Enum CharacterType, que definirá a cor do elfo. ModelInfo e ModelsDataManager usados ​​para serializar um Dicionário para *CharacterWarehouse*, este que será um singleton capaz de criar e instanciar cada um dos caracteres de acordo com o CharacterType. Esta Classe também controlará as estatísticas populacionais e quantos elfos de cada tipo foram gerados. Aqui encontraremos nosso principal controlador de desempenho, já que a função **CreateCharacter** só fará algo se o Frame Rate for maior que 20. Tornando possível para que cada dispositivo tenha uma grande quantidade de elfos existindo ao mesmo tempo, as animações rodam sem problemas.

**Extensions:** Este namespace não está sendo utilizado como um lugar para as classes de extensão como deveria ser, mas mais como utilitários. Aqui temos três arquivos:
-   _CameraExtensions.cs_: Uma classe estática real de Extensão, lar de métodos para calcular a proporção de tela com base na câmera do nosso jogo ou vetores proporcionais e floats.
-   _AspectRatioResizer.cs_: Um MonoBehaviour que recebe duas variáveis Vector3 mínima e máxima e usando CameraExtensions define uma nova escala proporcional para o transform anexado.
-   _PerformanceManager.cs_: Uma classe estática simples usada para fornecer informações de FPS e uso de memória para todo o jogo.

**SpawnSystem:** Aqui está tudo que precisamos para que as casas dos nossos elfos funcionem. Além disso, os comandos do jogador ocorrem aqui:
-   _SpawnerController.cs_: Anexado a cada prefab de Spawner, o _SpawnerController_ recebe os botões de mais e menos, um Transform para o ponto de spawn, o TimerView e o CharacterType que deve ser spawnado, todos como SerializedFields. Em seguida, ele utiliza esses elementos para gerenciar e aplicar a lógica necessária para spawnar um novo elfo e aumentar ou diminuir esse intervalo. O aumento e a diminuição são definidos pela constante SPAWN_INTERVAL_DELTA, que é definida como 1 segundo. Além disso, se o intervalo for zero, nenhum elfo será spawnado.
-   _TimerView.cs_: Este script é usado e chamado pelo *SpawnerController* para mostrar aos jogadores o intervalo em segundos que será usado entre cada spawn de elfo, no TMP no topo de cada spawner.

**UI:** Os dois scripts de visualização para ambos os prefabs de UI, o script _PerformanceUI.cs_ que atualizará o MemoryLog e o script _PopulationUI.cs_ que atualizará o prefab Population.

## Game Design & Performance Decisions
**EN-US 🇺🇸**

=> Spawners at 0s interval are turned off 
=> Initially players should only click on the spawner (using IPointerClickHandler) to increase the spawn time and after reaching the 10s interval the next click will be reset to 0, but this was changed to the less and more button allowing larger intervals 
=> the 1s step between each click on the + or - of the spawn is Arbitrary and defined in a constant. 
=> the 20 FPS limit to prevent character generation was made to allow multiple devices to have the largest possible number of characters on the board without losing the quality of the animations, the goal was to use 30 instead of 20, but 20 works very well since the characters are small on the screen and on mobile devices by default 30 FPS is used as the maximum refresh rate and thus no elf would be generated.

**PT-BR 🇧🇷**

=> Spawners em 0s de intervalo estão desligados
=> Inicialmente os jogadores deveriam apenas clicar no spawner (usando IPointerClickHandler) para aumentar o tempo de spawn e após atingir o intervalo de 10s o próximo clique será redefinido para 0, mas isso foi alterado para o botão menos e mais permitindo intervalos maiores
=> o passo de 1s entre cada clique no  + ou - do spawn é Arbitrário e definido em uma constante.
=> o limite de 20 FPS para impedir a geração de personagens foi feito para permitir que vários dispositivos tivessem a maior quantidade possível de personagens no tabuleiro sem perder a qualidade das animações, o objetivo era usar 30 em vez de 20, mas 20 funciona muito bem já que os personagens são pequenos na tela e nos dispositivos móveis por padrão se usa 30 FPS como taxa de atualização máxima e dessa forma nenhum elfo seria gerado.

## Benchmarks
**EN-US 🇺🇸**

Android and iOS versions are builded and tested on multiples devices. Here the focus is on Memory usage but also the max elves alive reached by each device, since the spawn limit is controlled by the FPS

**PT-BR 🇧🇷**

As versões Android e iOS foram construídas e testadas em vários dispositivos. Aqui o foco é no uso de memória, mas também no máximo de elfos vivos alcançados por cada dispositivo, já que o limite de spawn é controlado pelo FPS.

 - SAMSUNG GALAXY A54 (2023)
 - SAMSUNG GALAXY S23
 - SAMSUNG GALAXY S22
 - SAMSUNG A7 (2017)
 - MOTOROLA G50 5G
 - MOTOROLA G73
 - iPhone 12

## Known Issues
**EN-US 🇺🇸**

 - Some Devices not Showing UI TMPro (Samsung A7 & Motorola G73)

**PT-BR 🇧🇷**

- Alguns dispositivos não estão mostrando UI TMPro (Samsung A7 & Motorola G73)

## Possible Improves
 ### Arts & Animation 
 ### Code
