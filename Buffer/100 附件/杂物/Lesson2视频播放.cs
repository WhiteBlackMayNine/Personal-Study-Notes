using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Lesson2 : MonoBehaviour
{
    public RenderTexture texture;

    public VideoClip clip;

    VideoPlayer videoPlayer;

    private bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ Video Player��ʲô
        //Video Player����˼������Ƶ����������˼
        //����Unity�ṩ���������ڲ�����Ƶ�����
        //����Ƶ���������
        //��������Ϸ�в��ŵ������Ƶ�����ļ�
        #endregion

        #region ֪ʶ��� ���Video Player���
        //����һ����Hierarchy���ڵ���Ӻţ�ѡ��Video > Video Player
        //��������ѡ�񳡾����κ�һ������Ϊ�����Video Player���
        //��������ֱ�ӽ���Ƶ�ļ����뵽Hierarchy������
        #endregion

        #region ֪ʶ���� Video Player�������

        #endregion

        #region ֪ʶ���� Video Player�������
        //ע�⣺ʹ��VideoPlayer�����Ҫ���������ռ�UnityEngine.Video

        //1.��һ�� VideoPlayer ���ӵ����������
        //  �� VideoPlayer ��ӵ����������ʱ��
        //  �����Զ���׼��������壬������� videoPlayer.targetCamera��
        GameObject camera = GameObject.Find("Main Camera");
        videoPlayer = camera.AddComponent<VideoPlayer>();

        //2.�����������
        //  �Ƿ��Զ�����
        videoPlayer.playOnAwake = false;
        //  ��Ⱦģʽ
        videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
        //����Ŀ�� ��Ⱦ��ͼ
        //videoPlayer.targetTexture = texture;
        //����Ŀ�������
        //videoPlayer.targetCamera

        //  ͸����
        videoPlayer.targetCameraAlpha = 0.5f;
        //videoPlayer.targetCamera3DLayout = Video3DLayout.OverUnder3D;

        //  ��ƵԴ
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = clip;

        //videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = Application.streamingAssetsPath + "/Video.mp4";

        //  �Ƿ�ѭ��
        videoPlayer.isLooping = false;

        //  ��Ƶ��ʱ��
        print(videoPlayer.length);//��λΪs
        //  ��ǰʱ��,�����˶��
        print(videoPlayer.time);//��λΪs

        //  ��֡��
        print(videoPlayer.frameCount);
        //  ��ǰ֡
        print(videoPlayer.frame);


        //3.�������
        //  ���š�ֹͣ����ͣ

        //  ׼������
        videoPlayer.Prepare();

        //4.�¼����
        //  ׼������¼�
        videoPlayer.prepareCompleted += (v) =>
        {
            print("׼�����");
            isOver = true;
        };

        //  ��ʼ�¼�
        videoPlayer.started += (v) =>
        {
            print("��ִ��player���ŷ����� ����õ��¼�");
        };

        //  ��βʱ�����¼�
        videoPlayer.loopPointReached += (v) =>
        {
            print("��Ƶ���ŵ���β��ʱ����õ��¼�");
        };
        #endregion

        #region �ܽ�
        //VideoPlayer��������Ƶ���ŵ����
        //���ǿ���ͨ������������Ƶ�����������

        //���бȽ���Ҫ�Ĳ�����
        //1.��Ⱦģʽ
        //2.������ͣ���API

        //��Ƶ����ʱ����Ҫ�ж��ݵ�׼��ʱ��ŻῪʼ����
        //���ǿ���ͨ��Prepare����������׼����׼����Ϻ�����ʽ��ʼ����
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOver)
        {
            //��Ƶ����
            videoPlayer.Play();
        }

        if (Input.GetKeyDown(KeyCode.S) && isOver)
        {
            //��Ƶֹͣ
            videoPlayer.Stop();
        }

        if (Input.GetKeyDown(KeyCode.P) && isOver)
        {
            //��Ƶ��ͣ
            videoPlayer.Pause();
        }
    }
}
