using System.Text;
namespace AdventOfCode {
  public class PacketDecoder: IPuzzle {
    long version = 0;
    Dictionary<char, string> H2BMap = new Dictionary<char, string>();
    public void Run(){
      LoadDict();
      InputReader inputReader = new InputReader();
      var input = inputReader.GetInputAsString("PacketDecoder.txt");
      var packet = GetBinaryString(input[0]);
      var result = DecodePacket(packet);
      Console.WriteLine("First: " + version);
      Console.WriteLine("Second: "+ result.res);
    }

    private (int ctn, long res) DecodePacket(string packet){
      version += GetInt(packet, 0, 3);
      long type = GetInt(packet, 3, 3);
      if(type == 4){
        return ParseNumbers(packet, 6);
      }else{
        var res = packet[6] == '0' ? ParseAsLength(packet, 7, type) : ParseAsPackets(packet, 7, type);
        return (res.ctn, res.res);
      }
    }

    private (int ctn, long res) ParseAsLength(string packet, int idx, long type){
      long len = GetInt(packet, idx, 15);
      idx += 15;
      List<long> nums = new List<long>();
      while(len > 0){
        var result = DecodePacket(packet.Substring(idx));
        len -= result.ctn;
        idx += result.ctn;
        nums.Add(result.res);
      }
      long res = Eval(type, nums);
      return (idx, res);
    }

    private (int ctn, long res) ParseAsPackets(string packet, int idx, long type){
      long num = GetInt(packet, idx, 11);
      idx += 11;
      List<long> nums = new List<long>();
      while(num > 0){
        var result = DecodePacket(packet.Substring(idx));
        idx += result.ctn;
        num--;
        nums.Add(result.res);
      }
      long res = Eval(type, nums);
      return (idx, res);
    }

    private long GetInt(string packet, int idx, int num){
      string verBin = packet.Substring(idx, num);
      long ver = Convert.ToInt64(verBin, 2);
      return ver;
    }

    private (int idx, long res) ParseNumbers(string packet, int idx){
      long res = 0;
      StringBuilder binNum = new StringBuilder();
      while(idx < packet.Length){
        var temp = packet.Substring(idx, 5);
        binNum.Append(temp.Substring(1));
        idx += 5;
        if(temp[0] == '0') break;
      }
      res = GetInt(binNum.ToString(), 0, binNum.Length);
      return (idx, res);
    }

    private long Eval(long type, List<long> nums){
      long result = 0;
      switch(type){
        case 0:
          result = nums.Sum();
          break;
        case 1:
          result = 1;
          foreach(var i in nums) result *= i;
          break;
        case 2:
          result = nums.Min();
        break;
        case 3:
          result = nums.Max();
        break;
        case 5:
          result = nums[0] > nums[1] ? 1 : 0;
        break;
        case 6:
          result = nums[0] < nums[1] ? 1 : 0;
        break;
        case 7:
          result = nums[0] == nums[1] ? 1 : 0;
        break;
      }
      return result;
    }

    private void LoadDict(){
      H2BMap.Add('0', "0000");
      H2BMap.Add('1', "0001");
      H2BMap.Add('2', "0010");
      H2BMap.Add('3', "0011");
      H2BMap.Add('4', "0100");
      H2BMap.Add('5', "0101");
      H2BMap.Add('6', "0110");
      H2BMap.Add('7', "0111");
      H2BMap.Add('8', "1000");
      H2BMap.Add('9', "1001");
      H2BMap.Add('A', "1010");
      H2BMap.Add('B', "1011");
      H2BMap.Add('C', "1100");
      H2BMap.Add('D', "1101");
      H2BMap.Add('E', "1110");
      H2BMap.Add('F', "1111");
    }

    private string GetBinaryString(string hex){
      StringBuilder sb = new StringBuilder();
      foreach(var c in hex){
        sb.Append(H2BMap[c]);
      }
      return sb.ToString();
    }
  }
}
