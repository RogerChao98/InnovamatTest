# InnovamatTest
Prova de l'entrevista de feina per a Innovamat

## Introducció
Primerament, m'agradaria aclarir que no he pogut finalitzar al 100% totes les funcionalitats que proposava l'enunciat, en part, però sense que serveixi d'excusa, perquè no he pogut ser a casa en els últims 3 dies. He intentat especificar en els comentaris del codi quina era la meva idea en quant a les parts del programa que no estan desenvolupades.

## Formulació
En un inici he desenvolupat la part del enunciat i mostra d'opcions en quant a una pregunta inicial prenent un string random d'una llista de 10 opcions (del 0 al 9) i a partir d'aquesta elecció aleatòria mostrar en text el nombre seleccionat i mostrar 3 imatges de nombres aleatòries, sent una d'elles la opció correcta.

En un inici la mostra d'opcions la vaig desenvolupar usant assets externs com son imatges, i encara que més tard vaig veure que l'enunciat explícitament demanava no fer-ne servir no he pogut canviar-ho per les raons que he exposat en l'introducció. De totes maneres conec quina seria la solució adequada (no assignar una imatge al botó i fer-ho simplement amb un text que sigui el nombre com a string, per exemple "3, 4, 5...".

Posteriorment, he trobat la forma de canviar de color els botons clicats a verd quan l'opcio correcta ha sigut seleccionada i en vermell quan l'opció incorrecta ha sigut seleccionada. No he pogut implementar el mètode per a fer l'animació de sortida dels botons segons com s'especificava a l'enunciat però he deixat implementada i comentada una solució que podria ser viable usant un comptador i un booleà que determina si la resposta correcta ha estat seleccionada.

La part del marcador m'ha sigut impossible de desenvolupar pero trobo que caldria especificar dos comptadors, de victoria i de derrota i al final de cada partida (ja sigui quan el booleà victoria es true o quan el comptador d'errades és 2) incrementar-los respectivament:

  if victoria == true {comptadorVictoria = comptadorVictoria + 1}
  if comptadorErrades == 2 {comptadorDerrota = comptadorDerrota + 1}

I posteriorment mostrar-ho en pantalla, ja sigui al final de cada partida o constantment.

## Conclusió
Encara que és evident que no he complert amb les especificacions que el enunciat determinava i que les condicions en les que m'he trobat no són cap excusa, considero que en l'estat que he entregat la prova amb 2-3 dies més (els dies que m'ha estat impossible dedicar-m'hi al projecte) hauria pogut acabar-lo al menys per a complir els requeriments que el enunciat em demanava.
