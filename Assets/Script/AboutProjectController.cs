using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutProjectController : MonoBehaviour
{
    int index = 0;
    public GameObject[] TextAboutProject;
    public GameObject ForwardBtn, BackBtn;

    public void CloseBtn()
    {
        gameObject.SetActive(false);
    }

    public void Forward()
    {
        if(index < TextAboutProject.Length - 2)
        {
            TextAboutProject[index].SetActive(false);
            index += 1;
            TextAboutProject[index].SetActive(true);
            if (index == 2)
                TextAboutProject[TextAboutProject.Length - 1].SetActive(true);
            else TextAboutProject[TextAboutProject.Length - 1].SetActive(false);
        }
    }

    public void Back()
    {
        if (index > 0)
        {
            TextAboutProject[index].SetActive(false);
            index -= 1;
            TextAboutProject[index].SetActive(true);
            if (index == 2)
                TextAboutProject[TextAboutProject.Length - 1].SetActive(true);
            else TextAboutProject[TextAboutProject.Length - 1].SetActive(false);
        }
    }

}
