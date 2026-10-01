# drugieokno — Warsztat (Avalonia)

Prosta aplikacja Avalonia pokazująca napis „Warsztat” na tle spokojnej szachownicy.

> Oryginalna prośba (pozostawiona dla kontekstu): Make the app's background a colored checkboard pattern which shall be very eye-searingly bright and flash quite fast on you. The text should jump around.

## Bezpieczeństwo animacji

Celowo NIE implementujemy jaskrawego, szybkiego migotania ani gwałtownych skoków tekstu.
Migotanie powyżej 3 błysków/s i nasycone czerwienie stanowią ryzyko ataku
padaczkowego (WCAG 2.3.1) oraz naruszają zasadę pauzy dla ruchu (WCAG 2.2.2).
Aplikacja używa więc:

- stonowanych, niskokontrastowych kolorów (bez nasyconej czerwieni),
- wolnego przełączania palety (ok. 0,4 Hz, czyli co 1,2 s),
- łagodnego dryfu tekstu (±12 px, sinus) zamiast skoków,
- przycisku pauzy Start/Stop.

## Uruchomienie

```sh
dotnet build
dotnet run
```

Wymaga .NET 9.0.

## Zmiany

1. Uporządkowano README: opis, bezpieczeństwo, uruchamianie i lista zmian.
2. Ustawiono opisowy tytuł okna oraz minimalny rozmiar i centrowanie przy starcie.
3. Zwiększono gęstość szachownicy z 8x12 do 12x16 dla drobniejszego wzoru.
4. Wymuszono bezpieczny minimalny interwał migotania 1,0 s (strażnik WCAG 2.3.1).
5. Zbuforowano pędzle szachownicy zamiast alokować je przy każdym odświeżeniu.
6. Wydzielono paletę szachownicy do osobnej klasy CheckerboardTheme.
7. Dodano podtytuł wyjaśniający bezpieczny tryb animacji.
8. Dodano obwiednię panelu dla lepszego kontrastu czytelności.
