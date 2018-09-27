public static class Helpers
{
    public static int GetTileScore(bool n = false, bool e = false, bool s = false, bool w = false)
    {
        int score = 0;
        score += n ? 1 : 0;
        score += e ? 2 : 0;
        score += s ? 4 : 0;
        score += w ? 8 : 0;
        return score;
    }
}
