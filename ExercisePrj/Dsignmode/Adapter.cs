using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//适配器模式
namespace ExercisePrj.Dsignmode
{
    //统一接口
    public interface IMediaPlayer
    {
         void Play(string audioType, string filename);
    }
    //播放接口
    public interface IAdvanceMediaPlayer
    {
        void PlayVlc(string filename);
        void PlayMp4(string filename);
    }
    public class VlcPlayer :IAdvanceMediaPlayer
    {
        public void PlayVlc(string filename)
        {
            Console.WriteLine("play vlc");
        }
        public void PlayMp4(string filename)
        {

        }
    }
    public class Mp4Player:IAdvanceMediaPlayer
    {
        public void PlayVlc(string filename)
        {

        }
        public void PlayMp4(string filename)
        {
            Console.WriteLine("play mp4");
        }
    }
    //适配器类
    public class MediaAdapter : IMediaPlayer
    {
        IAdvanceMediaPlayer advancedMusicPlayer;
       public MediaAdapter(String audioType)
        {
            if (audioType=="vlc")
            {
                advancedMusicPlayer = new VlcPlayer();
            }
            else if (audioType=="mp4")
            {
                advancedMusicPlayer = new Mp4Player();
            }
        }

       public void Play(String audioType, String fileName)
        {
            if (audioType=="vlc")
            {
                advancedMusicPlayer.PlayVlc(fileName);
            }
            else if (audioType=="mp4")
            {
                advancedMusicPlayer.PlayMp4(fileName);
            }
        }
    }
    //实体类
    public class AutoPaly : IMediaPlayer
    {
        MediaAdapter mediaAdapter;
        public void Play(String audioType, String fileName)
        {
            if (audioType == "mp3")
            {
                Console.WriteLine("play mp3");
            }
            else if (audioType == "vlc" || audioType == "mp4")
            {
                mediaAdapter = new MediaAdapter(audioType);
                mediaAdapter.Play(audioType, fileName);
            }
            else
            {
                Console.WriteLine("invalid mediatype");
            }
        }
    }
}
