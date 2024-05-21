# Engima Simulator

A simulation of the [Enigma machine](https://en.wikipedia.org/wiki/Enigma_machine) implemented in C#.

## Usage

1. Specify a configuration (rotor wheels, plugboard, reflector, etc)
2. Construct a machine from that configuration
3. Encipher/decipher text using that machine

For example, the [Operation Barbarossa example](http://wiki.franklinheath.co.uk/index.php/Enigma/Sample_Messages#Operation_Barbarossa.2C_1941) from the excellent [Franklin Heath wiki](http://wiki.franklinheath.co.uk/index.php/Enigma) can be deciphered as follows:

```csharp
    var config = new Configuration();
    
    config.Rotors.Add(RotorName.II, 2);
    config.Rotors.Add(RotorName.IV, 21);
    config.Rotors.Add(RotorName.V, 12);

    config.Reflector = ReflectorName.RefB;
    
    config.PlugBoard.Add("AV BS CG DL FU HZ IN KM OW RX");
    
    var machine = config.Create();

    machine.Position = "BLA";

    var cipherText = "EDPUD NRGYS ZRCXN";  // truncated for brevity

    var plainText = machine.Encode(cipherText);

```

This and other examples may be found in [SampleMessageTests.cs](/tests/Enigma.Tests/SampleMessageTests.cs)

## Credits

This was made possible by the technical documentation made available by

- [Wikipedia](https://en.wikipedia.org/wiki/Enigma_machine)
- [Tony Sale](https://www.codesandciphers.org.uk/enigma/index.htm)
- [Franklin Heath](http://wiki.franklinheath.co.uk/index.php/Enigma)
- [Computerphile](https://github.com/mikepound/enigma)
