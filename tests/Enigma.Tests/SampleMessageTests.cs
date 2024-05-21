using FluentAssertions;
using Xunit.Abstractions;

namespace Enigma.Tests;

public class SampleMessageTests(ITestOutputHelper output)
{
    private void LogOutput(Machine machine) => machine.Log.Records
        .ForEach(x => output.WriteLine(x.ToString()));
    
    [Fact]
    public void TonySaleExample1()
    {
        // https://www.codesandciphers.org.uk/enigma/emachines/enigmad.htm
        var config = new Configuration();
        
        config.Rotors.Add(RotorName.IV, 'G');
        config.Rotors.Add(RotorName.II, 'M');
        config.Rotors.Add(RotorName.V, 'Y');
        config.Reflector = ReflectorName.RefB;
        
        // DN GR IS KC QX TM PV HY FW BJ
        config.PlugBoard.Add(
            new('D','N'), new('G','R'), new('I','S'), new('K','C'), new('Q','X'), 
            new('T','M'), new('P','V'), new('H','Y'), new('F','W'), new('B','J')
            );
        
        var machine = config.Create();
        
        //machine.Debug = true;

        machine.Position = "DHO";

        var key = machine.Encode("GXS");
        
        if (machine.Debug) LogOutput(machine);
        
        key.Should().Be("RLP");

        machine.Position = key;

        const string cipherText = "NQVLT YQFSE WWGJZ GQHVS EIXIM YKCNW IEBMB ATPPZ TDVCU PKAY";
        
        var plainText = machine.Encode(cipherText);
        
        output.WriteLine(plainText);
        
        machine.Position.Should().Be("RNM");
        
        const string expected = "FLUGZ EUGFU EHRER ISTOF WYYXF UELLG RAFXF UELLG PAFXP OFOP";
        
        plainText.Should().Be(expected);
    }

    [Fact]
    public void TonySaleExample2()
    {
        // https://www.codesandciphers.org.uk/enigma/emachines/enigmad.htm
        var config = new Configuration();

        config.Rotors.Add(RotorName.III, 'R');
        config.Rotors.Add(RotorName.V, 'X');
        config.Rotors.Add(RotorName.IV, 'O');
        config.Reflector = ReflectorName.RefB;

        // NP JV LY IX KQ AO DZ CR FT EM
        config.PlugBoard.Add(
            new('N','P'), new('J','V'), new('L','Y'), new('I','X'), new('K','Q'), 
            new('A','O'), new('D','Z'), new('C','R'), new('F','T'), new('E','M')
            );

        var machine = config.Create();

        machine.Position = "XNK";

        var key = machine.Encode("SLH");

        key.Should().Be("PNI");

        machine.Position = key;

        const string cipherText = "XAKEC EIMEU VXIIS JGFFH KHRMN QTLCZ OOOCL BSQDM";
        
        var result = machine.Encode(cipherText);
        
        output.WriteLine(result);
    }

    [Fact]
    public void InstructionManualExample()
    {
        // http://wiki.franklinheath.co.uk/index.php/Enigma/Sample_Messages#Enigma_Instruction_Manual.2C_1930
        
        var config = new Configuration();
        
        config.Rotors.Add(RotorName.II, RotorName.I, RotorName.III);
        config.Rotors.SetRings(24, 13, 22);
        config.PlugBoard.Add("AM FI NV PS TU WZ");
        config.Reflector = ReflectorName.RefA;
        
        var machine = config.Create();
        
        machine.Position = "ABL";
        
        output.WriteLine(machine.ToString());

        const string cipherText = "GCDSE AHUGW TQGRK VLFGX UCALX VYMIG MMNMF DXTGN VHVRM MEVOU YFZSL RHDRR XFJWC FHUHM UNZEF RDISI KBGPM YVXUZ";
        
        var result = machine.Encode(cipherText);
        
        output.WriteLine(result);
    }

    private static Machine CreateBarbarossaMachine()
    {
        var config = new Configuration();
        
        config.PlugBoard.Add("AV BS CG DL FU HZ IN KM OW RX");
        config.Rotors.Add(RotorName.II, RotorName.IV, RotorName.V);
        config.Rotors.SetRings(2, 21, 12);
        config.Reflector = ReflectorName.RefB;
        
        return config.Create();
    }

    [Fact]
    public void Barbarossa1()
    {
        // http://wiki.franklinheath.co.uk/index.php/Enigma/Sample_Messages#Operation_Barbarossa.2C_1941
        var machine = CreateBarbarossaMachine();
        
        machine.Position = "BLA";
        
        output.WriteLine(machine.ToString());

        const string cipherText =
            "EDPUD NRGYS ZRCXN UYTPO MRMBO FKTBZ REZKM LXLVE FGUEY SIOZV EQMIK UBPMM YLKLT TDEIS MDICA GYKUA CTCDO MOHWX MUUIA UBSTS LRNBZ SZWNR FXWFY SSXJZ VIJHI DISHP RKLKA YUPAD TXQSP INQMA TLPIF SVKDA SCTAC DPBOP VHJK";
        
        var plainText = machine.Encode(cipherText);
        
        output.WriteLine(plainText);
        
        output.WriteLine(machine.ToString());
        
        const string expected = 
            "AUFKL XABTE ILUNG XVONX KURTI NOWAX KURTI NOWAX NORDW ESTLX SEBEZ XSEBE ZXUAF FLIEG ERSTR ASZER IQTUN GXDUB ROWKI XDUBR OWKIX OPOTS CHKAX OPOTS CHKAX UMXEI NSAQT DREIN ULLXU HRANG ETRET ENXAN GRIFF XINFX RGTX";
        
        plainText.Should().Be(expected);
    }

    [Fact]
    public void Barbarossa2()
    {
        // http://wiki.franklinheath.co.uk/index.php/Enigma/Sample_Messages#Operation_Barbarossa.2C_1941
        var machine = CreateBarbarossaMachine();
        
        machine.Position = "LSD";
        
        output.WriteLine(machine.ToString());

        const string cipherText =
            "SFBWD NJUSE GQOBH KRTAR EEZMW KPPRB XOHDR OEQGB BGTQV PGVKB VVGBI MHUSZ YDAJQ IROAX SSSNR EHYGG RPISE ZBOVM QIEMM ZCYSG QDGRE RVBIL EKXYQ IRGIR QNRDN VRXCY YTNJR";
        
        var plainText = machine.Encode(cipherText);
        
        output.WriteLine(plainText);
        
        output.WriteLine(machine.ToString());
        
        const string expected = 
            "DREIG EHTLA NGSAM ABERS IQERV ORWAE RTSXE INSSI EBENN ULLSE QSXUH RXROE MXEIN SXINF RGTXD REIXA UFFLI EGERS TRASZ EMITA NFANG XEINS SEQSX KMXKM XOSTW XKAME NECXK";
        
        plainText.Should().Be(expected);
    }
}