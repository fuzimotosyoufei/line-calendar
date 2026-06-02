using Microsoft.AspNetCore.Mvc;
using ShiftManagerApi.Data;
using ShiftManagerApi.Models;
using Microsoft.EntityFrameworkCore;



namespace ShiftManagerApi.Controllers

{

    [ApiController]

    [Route("api/[controller]")] // これでURLが 「https://localhost:xxxx/api/shift」 になります

    public class ShiftController : ControllerBase

    {

        private readonly AppDbContext _context;

        // 💡 データベースへの窓口（Context）をコンストラクタで受け取る
        public ShiftController(AppDbContext context)
        {
            _context = context;
        }


        // 💡 1. スマホからの「提出」を受け取るAPI (POST)

        [HttpPost]//条件 Http側にpostでデータが来たら

        public async Task<IActionResult> SubmitShift([FromBody] ShiftSubmission submission)//asyrcは今から時間のかかる非同期処理を実行すると宣言している Taskは箱今裏で実行しているからこの箱に入れといてと宣言するためIactionResult型は処理が終わった後にエラーがちゃんとできたかを送るようにする便利な奴
                                                                                           //FromBodyは送られてくるデータを取り出してShiftSubmission型のsubmissionに入れ直すやつ　本当に入れたいリストに入れれるかを調べるために行う
        {
            if (submission == null || string.IsNullOrEmpty(submission.Name) || submission.Dates == null)
            {
                return BadRequest(new { message = "データが正しくありません。" });
            }

            // 💡 届いた日付リスト（例：["2026-06-15", "2026-06-16"]）を1日ずつバラして保存する
            foreach (var date in submission.Dates)
            {
                var entity = new ShiftEntity
                {
                    Name = submission.Name,
                    DateString = date
                };
                _context.Shifts.Add(entity); // データベースの仮保存リストに入れる
            }

            await _context.SaveChangesAsync(); // 完全にコミット（ガチ保存）！

            Console.WriteLine($"[DB保存完了] {submission.Name}さんから{submission.Dates.Count}日分のシフトを保存しました。");
            return Ok(new { message = "シフト希望をデータベースに登録しました！" });
        }


        // 💡 2. パソコンの管理画面に「提出された一覧」を返すAPI (GET)

        [HttpGet]

        [HttpGet]
        public async Task<IActionResult> GetAllShifts()
        {
            // データベースの全データを取得
            var allEntities = await _context.Shifts.ToListAsync();

            // 管理画面（フロント）が読みやすいように、名前ごとに日付をグループ化して整形する
            var groupedShifts = allEntities
                .GroupBy(s => s.Name)
                .Select(g => new
                {
                    Name = g.Key,
                    Dates = g.Select(s => s.DateString).OrderBy(d => d).ToList()
                })
                .ToList();

            return Ok(groupedShifts);
        }

        // スマホから届く荷物の形（受け取り用モデル）
        public class ShiftSubmission
        {
            public string Name { get; set; } = string.Empty;
            public List<string> Dates { get; set; } = new();
        }
    }

}