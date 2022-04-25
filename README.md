# Bewerbungsaufgabe

Für die Lösung der Aufgabe benötigen Sie keine grafische Oberfläche, Datenbank oder andere Infrastruktur. Wir legen Wert auf lesbaren Code der durch sinnvolle Tests geprüft wird. Verwenden Sie C++ oder C# für die Lösung der Aufgabe.  
Sie haben in einem Team von Softwareentwicklern die Aufgabe bekommen, ein Programm zu schreiben, das zeitbasierte Daten von einem Medizingerät verarbeiten kann. Die Datenpakete haben die folgende Struktur:

    class Measurement
    {
      private Instant measurementTime;
      private Double measurementValue;
      private MeasurementType type;
    }

Mögliche Messwerttypen sind z.B. Temperatur, SpO2, Herzrate etc. Die Messwerte können in einer beliebigen Genauigkeit vorliegen und werden im Sekundenbereich gemessen.  
Ihre Aufgabe: samplen Sie die sekundengenauen Messwerte auf ein 5 Minuten Raster. Dabei gelten die folgenden Regeln:

- Jeder Messwerttyp wird getrennt gesampled
- Aus einem Intervall von 5 Minuten wird nur der letzte Wert eines Typs ausgewählt
- Liegt der Wert genau auf dem Raster, zählt er zum aktuellen Intervall
- Die Eingabewerte sind zeitlich nicht sortiert
- Das Ergebnis des Samplings muss nach Zeit aufsteigend sortiert sein  


## Ein Beispiel:

    INPUT:
    {2017-01-03T10:04:45, TEMP, 35.79}
    {2017-01-03T10:01:18, SPO2, 98.78}
    {2017-01-03T10:09:07, TEMP, 35.01}
    {2017-01-03T10:03:34, SPO2, 96.49}
    {2017-01-03T10:02:01, TEMP, 35.82}
    {2017-01-03T10:05:00, SPO2, 97.17}
    {2017-01-03T10:05:01, SPO2, 95.08}
    OUTPUT:
    {2017-01-03T10:05:00, TEMP, 35.79}
    {2017-01-03T10:10:00, TEMP, 35.01}
    {2017-01-03T10:05:00, SPO2, 97.17}
    {2017-01-03T10:10:00, SPO2, 95.08}

Ein Teammitglied hat Ihnen schon eine mögliche Signatur verraten:

    public Map<MeasurementType, List<Measurement>> sample(Instant startOfSampling,
    List<Measurement> unsampledMeasurements) {
      // your implementation here
    }