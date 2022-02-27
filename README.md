# Automower Project
#### Med B.S

## Stryker
| Project | Score |
| ------ | ------ |
| Application |  [![Mutation testing badge](https://img.shields.io/endpoint?style=for-the-badge&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FMohammedBenSaidCoding%2FAMProject%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/MohammedBenSaidCoding/AMProject/main?module=application-layer ) |
| Domain |  [![Mutation testing badge](https://img.shields.io/endpoint?style=for-the-badge&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FMohammedBenSaidCoding%2FAMProject%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/MohammedBenSaidCoding/AMProject/main?module=domain-layer ) |
| Api | [![Mutation testing badge](https://img.shields.io/endpoint?style=for-the-badge&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FMohammedBenSaidCoding%2FAMProject%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/MohammedBenSaidCoding/AMProject/main?module=api-layer )|  

## Sonar
|  |  |
| ------ | ------ |
|[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=MohammedBenSaidCoding_AMProject&metric=bugs)](https://sonarcloud.io/summary/new_code?id=MohammedBenSaidCoding_AMProject)|[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=MohammedBenSaidCoding_AMProject&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=MohammedBenSaidCoding_AMProject)|
|[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=MohammedBenSaidCoding_AMProject&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=MohammedBenSaidCoding_AMProject)|[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=MohammedBenSaidCoding_AMProject&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=MohammedBenSaidCoding_AMProject)|
|[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=MohammedBenSaidCoding_AMProject&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=MohammedBenSaidCoding_AMProject)|[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=MohammedBenSaidCoding_AMProject&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=MohammedBenSaidCoding_AMProject)|
|[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=MohammedBenSaidCoding_AMProject&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=MohammedBenSaidCoding_AMProject)|[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=MohammedBenSaidCoding_AMProject&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=MohammedBenSaidCoding_AMProject)|


## Project description
The company X wants to develop an automower for square surfaces.
The mower can be programmed to go throughout the whole surface. Mower's
position is represented by coordinates (X,Y) and a characters indicate the orientation
according to cardinal notations (N,E,W,S). The lawn is divided in grid to simplify
navigation.
 
For example, the position can be 0,0,N, meaning the mower is in the lower left of the
lawn, and oriented to the north.
To control the mower, we send a simple sequence of characters. Possibles
characters are L,R,F. L and R turn the mower at 90° on the left or right without
moving the mower. F means the mower move forward from one space in the
direction in which it faces and without changing the orientation.
If the position after moving is outside the lawn, mower keep it's position. Retains its
orientation and go to the next command.
We assume the position directly to the north of (X,Y) is (X,Y+1).
To program the mower, we can provide an input file constructed as follows:
The first line correspond to the coordinate of the upper right corner of the lawn. The
bottom left corner is assumed as (0,0). The rest of the file can control multiple
mowers deployed on the lawn. Each mower has 2 next lines :
The first line give mower's starting position and orientation as "X Y O". X and Y being
the position and O the orientation.
The second line give instructions to the mower to go throughout the lawn.
Instructions are characters without spaces.
Each mower move sequentially, meaning that the second mower moves only when
the first has fully performed its series of instructions.
When a mower has finished, it give the final position and orientation.
 
## Technical section:
-  Programming language : C# 10
-  Framework : .NET 6
-  Version control : Github
-  Deployment: Github actions
 
 
###
## Architectural diagrams:
### DDD
![N|Solid](https://i.postimg.cc/Jhc4LBK2/DEM-Sch-mas-techniques-Frame-5-1.jpg)
### CQRS Mediator
![N|Solid](https://i.postimg.cc/bdffjpbX/DEM-Sch-mas-techniques-Frame-6.jpg)
### MediatR Pipeline
![N|Solid](https://i.postimg.cc/13P4GZdY/DEM-Sch-mas-techniques-Frame-7.jpg)
 
## Libraries
 
In this project I used the following libraries
 
| Library | README |
| ------ | ------ |
| AutoFixture | [AutoFixture][AutoFixture] |
| MediatR | [MediatR][MediatR] |
| Serilog | [Serilog][Serilog] |
| Fluent Assertions | [Fluent Assertions][fluentassertions] |
 
 
## License
 
MIT
 
**Mohammed BEN SAID**
 
[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)
 
   [dill]: https://github.com/joemccann/dillinger
  [git-repo-url]: https://github.com/joemccann/dillinger.git
   [john gruber]: http://daringfireball.net
   [df1]: http://daringfireball.net/projects/markdown/
   [markdown-it]: https://github.com/markdown-it/markdown-it
   [Ace Editor]: http://ace.ajax.org
   [node.js]: http://nodejs.org
   [Twitter Bootstrap]: http://twitter.github.com/bootstrap/
   [jQuery]: http://jquery.com
   [@tjholowaychuk]: http://twitter.com/tjholowaychuk
   [express]: http://expressjs.com
   [AngularJS]: http://angularjs.org
   [Gulp]: http://gulpjs.com
 
   [AutoFixture]: https://github.com/AutoFixture/AutoFixture
   [MediatR]: https://github.com/jbogard/MediatR/blob/master/README.md
   [Serilog]: https://github.com/serilog/serilog/blob/dev/README.md
   [fluentassertions]: https://github.com/fluentassertions/fluentassertions
